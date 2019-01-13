using System.Net.WebSockets;

namespace FirServer.Models.User
{
    public class UserInfo
    {
        public WebSocket socket { get; private set; }
        public long uid { get; set; }
        public string name { get; set; }

        public UserInfo(WebSocket socket)
        {
            this.socket = socket;
        }
    }
}
