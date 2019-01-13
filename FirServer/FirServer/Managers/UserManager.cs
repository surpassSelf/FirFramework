using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using FirServer.Models.User;

namespace FirServer.Managers
{
    public class UserManager : BaseBehaviour, IManager
    {
        private static Dictionary<long, UserInfo> users = new Dictionary<long, UserInfo>();
        public void Initialize()
        {
            UserMgr = this;
            users.Clear();
        }

        public UserInfo AddUser(long socketid, WebSocket socket)
        {
            UserInfo user = null;
            if (!users.ContainsKey(socketid))
            {
                user = new UserInfo(socket);
                users.Add(socketid, user);
            }
            else
            {
                throw new Exception("AddUser uid:>" + socketid);
            }
            return user;
        }

        public UserInfo GetUser(long socketid)
        {
            UserInfo user = null;
            users.TryGetValue(socketid, out user);
            return user;
        }

        public void RemoveUser(long socketid)
        {
            if (users.ContainsKey(socketid))
            {
                users.Remove(socketid);
            }
        }

        public void OnDispose()
        {
            UserMgr = null;
        }
    }
}
