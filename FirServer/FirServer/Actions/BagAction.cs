using System;
using LitJson;
using FirServer.Controllers;

namespace FirServer.Actions
{
    public class BagAction : BaseAction
    {
        public override ResultData OnExecute(string method, JsonData data)
        {
            switch(method)
            {
                case "buy": return OnBuy(data);
                case "sell": return OnSell(data);
                default: return new ResultData();
            }
        }

        private ResultData OnSell(JsonData data)
        {
            throw new NotImplementedException();
        }

        private ResultData OnBuy(JsonData data)
        {
            throw new NotImplementedException();
        }
    }
}
