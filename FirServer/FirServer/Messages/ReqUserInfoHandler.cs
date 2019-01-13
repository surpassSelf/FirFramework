using System.Collections.Generic;
using System.Net.WebSockets;
using LitJson;
using FirServer.Defines;
using FirServer.Models;
using WebSocketManager;

namespace FirServer.Messages
{
    class ReqUserInfoHandler : BaseMessageHandler
    {
        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            var json = new JsonData();

            var uid = long.Parse(message["uid"].ToString());
            var userModel = ModelMgr.GetModel(ModelNames.User) as UserModel;
            if (userModel != null)
            {
                var list = new List<string>();
                list.Add("uid=" + uid);
                var dataset = userModel.Query(list);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    var row = dataset.Tables[0].Rows[0];
                    json["result"] = (int)ResultCode.Success;
                    json["username"] = row["username"].ToString();
                }
                else
                {
                    json["result"] = (int)ResultCode.Failed;   //结果码
                }
            }
            else
            {
                json["result"] = (int)ResultCode.Failed;   //结果码
            }
            var retMssage = new WebSocketManager.Message()
            {
                CommandId = Protocal.ReqUserInfo,
                MessageType = MessageType.Json,
                Data = JsonMapper.ToJson(json)
            };
            await handler.SendMessageAsync(socket, retMssage);
        }
    }
}
