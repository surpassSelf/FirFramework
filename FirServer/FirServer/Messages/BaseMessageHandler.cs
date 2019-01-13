using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    public class BaseMessageHandler : BaseBehaviour, IMessageHandler
    {
        public async virtual void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
        }
    }
}
