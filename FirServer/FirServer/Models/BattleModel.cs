using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;

namespace FirServer.Models
{
    public class BattleModel : BaseModel
    {
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(BattleModel));
        public BattleModel() : base("battle")
        {
        }
    }
}
