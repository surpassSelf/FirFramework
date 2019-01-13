using LitJson;
using FirServer.Controllers;

namespace FirServer.Actions
{
    public class ActivityAction : BaseAction
    {
        public override ResultData OnExecute(string method, JsonData data)
        {
            switch(method)
            {
                case "": return OnOne(data);
                default: return new ResultData();
            }
        }

        private ResultData OnOne(JsonData data)
        {
            return new ResultData();
        }
    }
}
