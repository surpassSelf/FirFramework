using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using FirServer;

namespace WebSocketManager
{
    public abstract class WebSocketHandler : BaseBehaviour
    {
        public ConnectionManager connManager { get; set; }

        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            SerializationBinder = new JsonBinderWithoutAssembly()
        };

        /// <summary>
        /// The waiting remote invocations for Server to Client method calls.
        /// </summary>
        private Dictionary<Guid, TaskCompletionSource<InvocationResult>> _waitingRemoteInvocations = new Dictionary<Guid, TaskCompletionSource<InvocationResult>>();

        /// <summary>
        /// Gets the method invocation strategy.
        /// </summary>
        /// <value>The method invocation strategy.</value>
        public MethodInvocationStrategy MethodInvocationStrategy { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketHandler"/> class.
        /// </summary>
        /// <param name="ConnectionManager">The web socket connection manager.</param>
        /// <param name="methodInvocationStrategy">The method invocation strategy used for incoming requests.</param>
        public WebSocketHandler(ConnectionManager ConnectionManager, MethodInvocationStrategy methodInvocationStrategy)
        {
            _jsonSerializerSettings.Converters.Insert(0, new PrimitiveJsonConverter());
            connManager = ConnectionManager;
            MethodInvocationStrategy = methodInvocationStrategy;
        }

        /// <summary>
        /// Called when a client has connected to the server.
        /// </summary>
        /// <param name="socket">The web-socket of the client.</param>
        /// <returns>Awaitable Task.</returns>
        public virtual async Task OnConnected(WebSocket socket)
        {
            connManager.AddSocket(socket);

            await SendMessageAsync(socket, new Message()
            {
                CommandId = Protocal.Connect,
                MessageType = MessageType.Text,
                Data = connManager.GetId(socket).ToString()
            }).ConfigureAwait(false);
        }

        public virtual async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, JsonData message)
        {
            await Task.Yield();
        }

        /// <summary>
        /// Called when a client has disconnected from the server.
        /// </summary>
        /// <param name="socket">The web-socket of the client.</param>
        /// <returns>Awaitable Task.</returns>
        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await connManager.RemoveSocket(connManager.GetId(socket)).ConfigureAwait(false);
        }

        public async Task SendMessageAsync(WebSocket socket, Message message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            var serializedMessage = JsonConvert.SerializeObject(message, _jsonSerializerSettings);
            var encodedMessage = Encoding.UTF8.GetBytes(serializedMessage);
            await socket.SendAsync(buffer: new ArraySegment<byte>(array: encodedMessage,
                                                                  offset: 0,
                                                                  count: encodedMessage.Length),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None).ConfigureAwait(false);
        }

        public async Task SendMessageAsync(long socketId, Message message)
        {
            await SendMessageAsync(connManager.GetSocketById(socketId), message).ConfigureAwait(false);
        }

        public async Task SendMessageToAllAsync(Message message)
        {
            foreach (var pair in connManager.GetAll())
            {
                try
                {
                    if (pair.Value.State == WebSocketState.Open)
                        await SendMessageAsync(pair.Value, message).ConfigureAwait(false);
                }
                catch (WebSocketException e)
                {
                    if (e.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
                    {
                        await OnDisconnected(pair.Value);
                    }
                }
            }
        }

        public async Task SendMessageToGroupAsync(string groupID, Message message)
        {
            var sockets = connManager.GetAllFromGroup(groupID);
            if (sockets != null)
            {
                foreach (var socket in sockets)
                {
                    await SendMessageAsync(socket, message);
                }
            }
        }

        public async Task SendMessageToGroupAsync(string groupID, Message message, long except)
        {
            var sockets = connManager.GetAllFromGroup(groupID);
            if (sockets != null)
            {
                foreach (var id in sockets)
                {
                    if (id != except)
                        await SendMessageAsync(id, message);
                }
            }
        }

        //public async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, Message receivedMessage)
        //{
        //    // method invocation request.
        //    if (receivedMessage.MessageType == MessageType.MethodInvocation)
        //    {
        //        // retrieve the method invocation request.
        //        InvocationDescriptor invocationDescriptor = null;
        //        try
        //        {
        //            invocationDescriptor = JsonConvert.DeserializeObject<InvocationDescriptor>(receivedMessage.Data, _jsonSerializerSettings);
        //            if (invocationDescriptor == null) return;
        //        }
        //        catch { return; } // ignore invalid data sent to the server.

        //        // if the unique identifier hasn't been set then the client doesn't want a return value.
        //        if (invocationDescriptor.Identifier == Guid.Empty)
        //        {
        //            // invoke the method only.
        //            try
        //            {
        //                await MethodInvocationStrategy.OnInvokeMethodReceivedAsync(socket, invocationDescriptor);
        //            }
        //            catch (Exception)
        //            {
        //                // we consume all exceptions.
        //            }
        //        }
        //        else
        //        {
        //            // invoke the method and get the result.
        //            InvocationResult invokeResult;
        //            try
        //            {
        //                // create an invocation result with the results.
        //                invokeResult = new InvocationResult()
        //                {
        //                    Identifier = invocationDescriptor.Identifier,
        //                    Result = await MethodInvocationStrategy.OnInvokeMethodReceivedAsync(socket, invocationDescriptor),
        //                    Exception = null
        //                };
        //            }
        //            // send the exception as the invocation result if there was one.
        //            catch (Exception ex)
        //            {
        //                invokeResult = new InvocationResult()
        //                {
        //                    Identifier = invocationDescriptor.Identifier,
        //                    Result = null,
        //                    Exception = new RemoteException(ex)
        //                };
        //            }

        //            // send a message to the client containing the result.
        //            var message = new Message()
        //            {
        //                MessageType = MessageType.MethodReturnValue,
        //                Data = JsonConvert.SerializeObject(invokeResult, _jsonSerializerSettings)
        //            };
        //            await SendMessageAsync(socket, message).ConfigureAwait(false);
        //        }
        //    }
        //    // method return value.
        //    else if (receivedMessage.MessageType == MessageType.MethodReturnValue)
        //    {
        //        var invocationResult = JsonConvert.DeserializeObject<InvocationResult>(receivedMessage.Data, _jsonSerializerSettings);
        //        // find the completion source in the waiting list.
        //        if (_waitingRemoteInvocations.ContainsKey(invocationResult.Identifier))
        //        {
        //            // set the result of the completion source so the invoke method continues executing.
        //            _waitingRemoteInvocations[invocationResult.Identifier].SetResult(invocationResult);
        //            // remove the completion source from the waiting list.
        //            _waitingRemoteInvocations.Remove(invocationResult.Identifier);
        //        }
        //    }
        //}
    }
}
