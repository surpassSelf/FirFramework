using LitJson;
using log4net;
using System.Collections.Generic;
using System.Net.WebSockets;
using FirServer.Defines;
using FirServer.Models;
using WebSocketManager;

namespace FirServer.Messages
{
    public class RegisterHandler : BaseMessageHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(RegisterHandler));

        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            long uid = 0L;
            var data = message["Data"];
            var username = data["username"].ToString();
            var password = data["password"].ToString();

            var values = new List<string>();
            values.Add("\"" + username + "\"");
            values.Add("\"" + password + "\"");

            var userModel = ModelMgr.GetModel(ModelNames.User) as UserModel;
            if (userModel != null)
            {
                uid = userModel.AddUser(values);
            }
            var result = uid == 0 ? (int)ResultCode.Failed : (int)ResultCode.Success;

            var json = new JsonData();
            json["result"] = result;   //结果码
            if (uid >= 0L)
            {
                json["uid"] = uid;      //用户ID
            }
            var retMssage = new WebSocketManager.Message()
            {
                CommandId = Protocal.Register,
                MessageType = MessageType.Json,
                Data = JsonMapper.ToJson(json)
            };
            await handler.SendMessageAsync(socket, retMssage);

            logger.Info("OnMessage: " + uid);
        }
    }
}
