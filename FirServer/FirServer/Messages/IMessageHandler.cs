using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    public interface IMessageHandler
    {
        void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message);
    }
}
