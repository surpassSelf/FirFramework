using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    class DefaultMessageHandler : BaseMessageHandler
    {
        public override void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
        }
    }
}
