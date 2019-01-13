using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LitJson;
using FirServer.Controllers;

namespace FirServer.Actions
{
    public class ShopAction : BaseAction
    {
        public override ResultData OnExecute(string method, JsonData data)
        {
            switch (method)
            {
                case "buy": return OnBuy(data);
                case "sell": return OnSell(data);
                default: return new ResultData();
            }
        }

        private ResultData OnBuy(JsonData data)
        {
            return new ResultData();
        }

        private ResultData OnSell(JsonData data)
        {
            return new ResultData();
        }
    }
}
