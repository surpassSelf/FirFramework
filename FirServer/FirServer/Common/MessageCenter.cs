using System.Collections.Generic;
using System.Net.WebSockets;
using LitJson;
using log4net;
using FirServer.Messages;
using WebSocketManager;

namespace FirServer.Common
{
    public class MessageCenter
    {
        private static MessageCenter _Instance;
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(MessageCenter));
        private static readonly Dictionary<Protocal, IMessageHandler> mHandlers = new Dictionary<Protocal, IMessageHandler>();

        public static MessageCenter Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MessageCenter();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 初始化消息处理器映射
        /// </summary>
        public void Initialize()
        {
            mHandlers.Add(Protocal.Login, new LoginHandler());
            mHandlers.Add(Protocal.Register, new RegisterHandler());
            mHandlers.Add(Protocal.Default, new DefaultMessageHandler());
            mHandlers.Add(Protocal.Disconnect, new DisconnectHandler());
            mHandlers.Add(Protocal.ReqUserInfo, new ReqUserInfoHandler());
            mHandlers.Add(Protocal.Battle, new BattleHandler());
            mHandlers.Add(Protocal.ReqLobbyList, new ReqLobbyListHandler());
            mHandlers.Add(Protocal.ReqLobbyEnter, new ReqLobbyEnterHandler());
            mHandlers.Add(Protocal.ReqMate, new ReqMateHandler());
            mHandlers.Add(Protocal.ReqRoomInfo, new ReqRoomInfoHandler());
            mHandlers.Add(Protocal.ReqRoomEnter, new ReqRoomEnterHandler());
            mHandlers.Add(Protocal.Logout, new LogoutHandler());
            mHandlers.Add(Protocal.GameStart, new ReqGameStartHandler());
            mHandlers.Add(Protocal.GameOver, new ReqGameOverHandler());
            mHandlers.Add(Protocal.ReqExitLobby, new ReqExitLobbyHandler());
            mHandlers.Add(Protocal.ReqExitRoom, new ReqExitRoomHandler());

            logger.Info("Initialize Success!!!");
        }

        public void OnRecvData(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            var commandId = message["CommandId"].ToString();
            Protocal key = (Protocal)int.Parse(commandId);
            logger.Info("ReceiveAsync[message.CommandId]:" + key);

            if (mHandlers.ContainsKey(key))
            {
                mHandlers[key].OnMessage(socket, handler, message);
            }
            else
            {
                mHandlers[Protocal.Default].OnMessage(socket, handler, message);
            }
        }

        public void OnDispose()
        {
        }
    }
}
