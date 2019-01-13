using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    public class ReqExitRoomHandler : BaseMessageHandler
    {
        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            base.OnMessage(socket, handler, message);
        }
    }
}
