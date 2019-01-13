using LitJson;
using FirServer.Controllers;

namespace FirServer.Actions
{
    public class RankAction : BaseAction
    {
        public override ResultData OnExecute(string method, JsonData data)
        {
            switch(method)
            {
                case "money": return OnMoney(data);
                default: return new ResultData();
            }
        }

        private ResultData OnMoney(JsonData data)
        {
            return new ResultData();
        }
    }
}
