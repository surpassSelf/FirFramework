using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    public class ReqLobbyListHandler : BaseMessageHandler
    {
        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            base.OnMessage(socket, handler, message);

            var lobbys = LobbyMgr.GetLobbys();
            var json = new JsonData();
            json["result"] = (int)ResultCode.Success;

            int i = 0;
            var subjson = new JsonData(); 
            foreach(var de in lobbys)
            {
                subjson[i]["name"] = de.Value.lobbyName;
                subjson[i]["room"] = de.Value.roomCount;
                subjson[i]["user"] = de.Value.userCount;
                i++;
            }
            json["data"] = subjson;
            var retMssage = new WebSocketManager.Message()
            {
                CommandId = Protocal.ReqLobbyList,
                MessageType = MessageType.Json,
                Data = JsonMapper.ToJson(json)
            };
            await handler.SendMessageAsync(socket, retMssage);
        }
    }
}
