using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    public class ReqLobbyEnterHandler : BaseMessageHandler
    {
        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            base.OnMessage(socket, handler, message);

            var data = message["Data"];
            var lobbyid = data["lobbyid"].ToString();

            var lobby = LobbyMgr.GetLobby(lobbyid);
            if (lobby != null)
            {
                lobby.OnEnter(socket);
            }
        }
    }
}
