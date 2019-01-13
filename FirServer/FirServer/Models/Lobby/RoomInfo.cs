using System.Collections.Generic;
using System.Net.WebSockets;
using FirServer.Models.User;

namespace FirServer.Models.Lobby
{
    public class RoomInfo : BaseBehaviour
    {
        public int RoomId { get; set; }
        private Dictionary<long, UserInfo> users = new Dictionary<long, UserInfo>();

        public RoomInfo(int id)
        {
            RoomId = id;
        }

        public int GetUserCount()
        {
            return users.Count;
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        public bool OnEnter(WebSocket socket)
        {
            var user = GetUserByWebSocket(socket); 
            if (user != null)
            {
                users.Add(user.uid, user);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 离开房间
        /// </summary>
        public bool OnLeave(WebSocket socket)
        {
            var user = GetUserByWebSocket(socket);
            if (user != null)
            {
                return users.Remove(user.uid);
            }
            return false;
        }
    }
}
