using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using LitJson;
using log4net;
using Utility;
using FirServer.Common;
using FirServer.Controllers;
using FirServer.Defines;
using FirServer.Models;
using WebSocketManager;

namespace FirServer.Actions
{
    public class UserAction : BaseAction
    {
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(UserAction));

        public override ResultData OnExecute(string method, JsonData data)
        {
            switch(method)
            {
                case "login": return OnLogin(data);
                case "register": return OnRegister(data);
                case "friendata": return OnFriendData(data);
                default: return new ResultData();
            }
        }

        /// <summary>
        /// 选择关卡
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ResultData OnFriendData(JsonData data)
        {
            logger.Info(data.ToString());
            return new ResultData(ResultCode.Success, new JsonData());
        }

        /// <summary>
        /// 登录信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ResultData OnLogin(JsonData data)
        {
            var rescode = data["code"].ToString();
            var nickname = data["nickname"].ToString();
            var wxUrl = "https://api.weixin.qq.com/sns/jscode2session?appid=" + AppConst.appid +
                        "&secret=" + AppConst.secret + "&js_code=" + rescode + "&grant_type=authorization_code";

            logger.Warn(wxUrl);
            var wxResult = HttpUtility.ReqHttp(wxUrl);

            var uid = string.Empty;
            var json = new JsonData();
            var result = ResultCode.Failed;
            if (!string.IsNullOrEmpty(wxResult))
            {
                var wxdata = JsonMapper.ToObject(wxResult);
                var openid = wxdata["openid"].ToString();
                var session_key = wxdata["session_key"].ToString();
                logger.Warn("wx result session_key:" + session_key + " openid:" + openid);

                var userModel = ModelMgr.GetModel(ModelNames.User) as UserModel;
                if (userModel != null)
                {
                    uid = userModel.ExistUser(openid);
                    var lastime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                    if (string.IsNullOrEmpty(uid))
                    {
                        var avatarUrl = data["avatarUrl"].ToString();
                        avatarUrl = System.Web.HttpUtility.UrlEncode(avatarUrl, Encoding.UTF8);

                        var values = new List<string>();
                        values.Add("str:" + openid);
                        values.Add("str:" + nickname);
                        values.Add("int:" + data["gender"]);     //money
                        values.Add("str:" + data["language"]);        //cannonid
                        values.Add("str:" + data["city"]);        //cannonid
                        values.Add("str:" + data["province"]);        //cannonid
                        values.Add("str:" + avatarUrl);        //cannonid
                        values.Add("str:" + data["brand"]);        //cannonid
                        values.Add("str:" + data["model"]);        //cannonid
                        values.Add("str:" + data["version"]);        //cannonid
                        values.Add("str:" + data["system"]);        //cannonid
                        values.Add("str:" + data["platform"]);        //cannonid
                        values.Add("int:1");
                        values.Add("str:" + lastime);
                        userModel.AddUser(values);  
                        result = ResultCode.Success;
                    }
                    else
                    {
                        result = ResultCode.ExistUser;  //用户已注册
                        var count = userModel.GetCount(openid);
                        userModel.SetCount(openid, count + 1);
                        userModel.SetLastTime(openid, lastime);
                    }
                    json["openid"] = openid;
                }
            }
            return new ResultData(result, json);
        }



        /// <summary>
        /// 注册信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ResultData OnRegister(JsonData data)
        {
            var result = ResultCode.Failed;

            var username = data["username"].ToString();
            var password = data["password"].ToString();

            long uid = 0L;
            var userModel = ModelMgr.GetModel(ModelNames.User) as UserModel;
            if (userModel != null)
            {
                var list = new List<string>();
                list.Add("username=\"" + username);
                list.Add("password=\"" + password);

                //uid = userModel.ExistUser(list);
                if (uid == 0L)
                {
                    var values = new List<string>();
                    values.Add(username);
                    values.Add(password);
                    values.Add("1000");     //money
                    values.Add("1");        //cannonid
                    values.Add("1");        //headid

                    uid = userModel.AddUser(values);
                    result = ResultCode.Success;
                }
                else
                {
                    result = ResultCode.ExistUser;  //用户已注册
                }
            }
            var json = new JsonData();
            if (uid >= 0L)
            {
                json["uid"] = uid;      //用户ID
            }
            return new ResultData(result, json);
        }
    }
}
