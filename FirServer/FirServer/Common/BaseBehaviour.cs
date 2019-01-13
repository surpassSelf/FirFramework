
using System.Net.WebSockets;
using ChatApplication;
using FirServer.Managers;
using FirServer.Models.User;

namespace FirServer
{
    public class BaseBehaviour
    {
        protected UserInfo GetUserByWebSocket(WebSocket socket)
        {
            var connMgr = AppServer.Instance.connManager;
            var socketid = connMgr.GetId(socket);
            return UserMgr.GetUser(socketid);
        }
        protected static DataManager DataMgr { get; set; }
        protected static TimerManager TimerMgr { get; set; }
        protected static ModelManager ModelMgr { get; set; }
        protected static ConfigManager ConfigMgr { get; set; }
        protected static BattleManager BattleMgr { get; set; }
        protected static LobbyManager LobbyMgr { get; set; }
        protected static UserManager UserMgr { get; set; }
        protected static ActionManager ActionMgr { get; set; }
    }
}
