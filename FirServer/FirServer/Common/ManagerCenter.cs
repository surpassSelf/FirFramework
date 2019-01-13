using System.Collections.Generic;
using log4net;
using FirServer.Defines;
using FirServer.Managers;

namespace FirServer.Common
{
    public class ManagerCenter
    {
        private static ManagerCenter _Instance;
        private static readonly Dictionary<string, IManager> mManagers = new Dictionary<string, IManager>();
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(ManagerCenter));

        public static ManagerCenter Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ManagerCenter();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// 初始化管理器
        /// </summary>
        public void Initialize()
        {
            mManagers.Add(ManagerNames.DATA, new DataManager());
            mManagers.Add(ManagerNames.TIMER, new TimerManager());
            mManagers.Add(ManagerNames.MODEL, new ModelManager());
            mManagers.Add(ManagerNames.BATTLE, new BattleManager());
            mManagers.Add(ManagerNames.USER, new UserManager());
            mManagers.Add(ManagerNames.CONFIG, new ConfigManager());
            mManagers.Add(ManagerNames.LOBBY, new LobbyManager());
            mManagers.Add(ManagerNames.ACTION, new ActionManager());

            foreach (var de in mManagers)
            {
                if (de.Value != null)
                {
                    de.Value.Initialize();
                }
            }
            logger.Info("Initialize Success!!!");
        }

        /// <summary>
        /// 获取管理器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IManager GetManager(string name)
        {
            IManager manager = null;
            mManagers.TryGetValue(name, out manager);
            return manager;
        }

        public void OnDispose()
        {
            foreach (var de in mManagers)
            {
                if (de.Value != null)
                {
                    de.Value.OnDispose();
                }
            }
        }
    }
}
