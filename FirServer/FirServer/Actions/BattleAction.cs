using System;
using System.Collections.Generic;

using LitJson;
using Utility;
using FirServer.Controllers;
using FirServer.Defines;
using WebSocketManager;

namespace FirServer.Actions
{
    public class BattleAction : BaseAction
    {
        private Dictionary<long, List<FishInfo>> mFishData = new Dictionary<long, List<FishInfo>>();
        private Dictionary<long, List<BulletData>> mBulletData = new Dictionary<long, List<BulletData>>();

        public override ResultData OnExecute(string method, JsonData data)
        {
            switch (method)
            {
                case "createfish": return OnCreateFish(data);
                case "attackfish": return OnAttackFish(data);
                default: return new ResultData();
            }
        }

        /// <summary>
        /// 创造鱼
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ResultData OnCreateFish(JsonData data)
        {
            var uid = Convert.ToInt64(data["uid"]);
            List<FishInfo> fish = null;
            if (!mFishData.ContainsKey(uid))
            {
                fish = new List<FishInfo>();
                mFishData.Add(uid, fish);
            }
            else
            {
                fish = mFishData[uid];
            }
            var json = new JsonData();
            for(int i = 0; i < 10; i++)
            {
                var fishData = new FishInfo();
                fishData.id = AppUtil.NewGuidId();
                fishData.type = AppUtil.Random(0, 10);
                fishData.health = 100;      //鱼的血值，需要配置

                json[i]["id"] = fishData.id;
                json[i]["type"] = fishData.type;
            }
            return new ResultData(ResultCode.Success, json);
        }

        /// <summary>
        /// 攻击鱼
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ResultData OnAttackFish(JsonData data)
        {
            var uid = Convert.ToInt64(data["uid"]);
            var fid = Convert.ToInt64(data["fid"]);
            var bid = Convert.ToInt64(data["bid"]);

            var json = new JsonData();
            return new ResultData(ResultCode.Success, json);
        }
    }
}
