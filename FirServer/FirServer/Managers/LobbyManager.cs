using System;
using System.Collections.Generic;
using FirServer.Models.Lobby;

namespace FirServer.Managers
{
    public class LobbyManager : BaseBehaviour, IManager
    {
        private Dictionary<string, LobbyInfo> lobbys = new Dictionary<string, LobbyInfo>();
        public void Initialize()
        {
            LobbyMgr = this;

            var config = ConfigMgr.GetGlobalConfig();

            var lobbys = config.lobbyList;
            for(int i = 0; i < lobbys.Count; i++)
            {
                var lobby = lobbys[i];
                AddLobby(lobby.id.ToString(), lobby.name, lobby.maxUserCount, lobby.maxRoomCount);
            }
        }

        /// <summary>
        /// 添加大厅
        /// </summary>
        /// <param name="lobby">大厅类型标识</param>
        /// <param name="name">大厅名字：如初级场</param>
        /// <param name="userCount">大厅最大人数</param>
        /// <param name="roomCount">房间个数</param>
        /// <param name="roomUserCount">房间人数限制</param>
        public void AddLobby(string strKey, string lobbyName, int userCount, int roomCount)
        {
            var lobby = new LobbyInfo();
            lobby.lobbyName = lobbyName;
            lobby.userCount = userCount;
            lobby.roomCount = roomCount;
            lobby.InitRooms();

            lobbys.Add(strKey, lobby);
        }

        public Dictionary<string, LobbyInfo> GetLobbys()
        {
            return lobbys;
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public LobbyInfo GetLobby(string name)
        {
            if (lobbys.ContainsKey(name))
            {
                return lobbys[name];
            }
            return null;
        }

        public void OnDispose()
        {
            lobbys.Clear();
            LobbyMgr = null;
        }
    }
}
