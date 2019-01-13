using System.Net.WebSockets;
using System.Threading.Tasks;
using LitJson;
using log4net;
using FirServer.Common;
using WebSocketManager;
using FirServer;

namespace ChatApplication
{
    public class AppServer : WebSocketHandler
    {
        public static AppServer Instance = null;
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(WebSocketHandler));

        public AppServer(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager, new ControllerMethodInvocationStrategy())
        {
            Instance = this;
            ((ControllerMethodInvocationStrategy)MethodInvocationStrategy).Controller = this;
        }

        public void Initialize()
        {
            MessageCenter.Instance.Initialize();
            ManagerCenter.Instance.Initialize();
        }

        public void OnDispose()
        {
            MessageCenter.Instance.OnDispose();
            ManagerCenter.Instance.OnDispose();
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = connManager.GetId(socket);
            logger.Info("Client: " + socketId + " OnConnected!");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, JsonData message)
        {
            MessageCenter.Instance.OnRecvData(socket, this, message);
            await base.ReceiveAsync(socket, result, message);
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketId = connManager.GetId(socket);
            await base.OnDisconnected(socket);

            UserMgr.RemoveUser(socketId);   //移除用户

            var message = new Message()
            {
                CommandId = Protocal.Disconnect,
                MessageType = MessageType.Text,
                Data = $"{socketId} disconnected"
            };
            await SendMessageToAllAsync(message);
        }
    }
}