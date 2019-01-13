using System.Collections.Generic;
using System.Net.WebSockets;

namespace FirServer.Models.Lobby
{
    public class LobbyInfo : BaseBehaviour
    {
        public string lobbyName;
        public int userCount;
        public int roomCount;
        private Dictionary<int, RoomInfo> rooms = new Dictionary<int, RoomInfo>();

        /// <summary>
        /// 创建房间
        /// </summary>
        public void InitRooms()
        {
            for (int i = 0; i < roomCount; i++)
            {
                rooms.Add(i, new RoomInfo(i));
            }
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <returns></returns>
        public RoomInfo GetRoom(int roomid)
        {
            RoomInfo room = null;
            rooms.TryGetValue(roomid, out room);
            return room;
        }

        /// <summary>
        /// 匹配
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public RoomInfo OnMate(WebSocket socket)
        {
            foreach(var r in rooms)
            {
                if (r.Value.GetUserCount() < 1)
                {
                    return r.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 进入大厅
        /// </summary>
        public void OnEnter(WebSocket socket)
        {
        }

        /// <summary>
        /// 离开大厅
        /// </summary>
        public void OnLeave(WebSocket socket)
        {
        }
    }
}
