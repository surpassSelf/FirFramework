using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirServer.Managers
{
    public class BattleManager : BaseBehaviour, IManager
    {
        public void Initialize()
        {
            BattleMgr = this;
        }

        public void OnDispose()
        {
            BattleMgr = null;
        }
    }
}
