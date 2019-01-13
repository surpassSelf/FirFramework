using System.Net.WebSockets;
using LitJson;
using log4net;
using FirServer.Models.Lobby;
using WebSocketManager;

namespace FirServer.Messages
{
    public class ReqMateHandler : BaseMessageHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(ReqMateHandler));

        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            base.OnMessage(socket, handler, message);

            var data = message["Data"];
            var lobbyid = data["lobbyid"].ToString();

            RoomInfo room = null;
            var lobby = LobbyMgr.GetLobby(lobbyid);
            if (lobby != null)
            {
                room = lobby.OnMate(socket);
            }
            var json = new JsonData();
            if (room == null)
            {
                json["result"] = (int)ResultCode.Failed;
            }
            else
            {
                json["result"] = (int)ResultCode.Success;
                json["roomid"] = room.RoomId;
            }
            var retMssage = new WebSocketManager.Message()
            {
                CommandId = Protocal.ReqMate,
                MessageType = MessageType.Json,
                Data = JsonMapper.ToJson(json)
            };
            await handler.SendMessageAsync(socket, retMssage);
            logger.Info("OnMessage: " + json["result"]);
        }
    }
}
