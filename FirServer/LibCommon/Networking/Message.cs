
namespace WebSocketManager
{
    public enum MessageType
    {
        Text = 0,
        Json = 1,
        Binrary = 2
    }

    public class Message
    {
        public Protocal CommandId { get; set; }
        public MessageType MessageType { get; set; }
        public string Data { get; set; }
    }
}