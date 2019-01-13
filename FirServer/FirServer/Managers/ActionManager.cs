using System.Collections.Generic;
using FirServer.Actions;
using FirServer.Defines;

namespace FirServer.Managers
{
    public class ActionManager : BaseBehaviour, IManager
    {
        private Dictionary<string, IAction> actions = new Dictionary<string, IAction>();
        public void Initialize()
        {
            ActionMgr = this;
            actions.Add(ActionNames.User, new UserAction());
            actions.Add(ActionNames.Battle, new BattleAction());
            actions.Add(ActionNames.Shop, new ShopAction());
        }

        /// <summary>
        /// 获取ACTION
        /// </summary>
        /// <param name="actName"></param>
        /// <returns></returns>
        public IAction GetAction(string actName)
        {
            IAction action = null;
            actions.TryGetValue(actName, out action);
            return action;
        }

        public void OnDispose()
        {
            actions.Clear();
            ActionMgr = null;
        }
    }
}
