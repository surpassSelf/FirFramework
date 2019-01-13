using System.Linq;
using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    class DisconnectHandler : BaseMessageHandler
    {
        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
        }
    }
}
