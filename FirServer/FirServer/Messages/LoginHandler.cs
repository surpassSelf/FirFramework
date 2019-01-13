using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using LitJson;
using log4net;
using FirServer.Defines;
using FirServer.Models;
using WebSocketManager;

namespace FirServer.Messages
{
    public class LoginHandler : BaseMessageHandler
    {
        private WebSocketHandler handler;
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(LoginHandler));

        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            this.handler = handler;

            var data = message["Data"];
            var username = data["username"].ToString();
            var password = data["password"].ToString();

            var uid = 0L;
            var json = new JsonData();
            var userModel = ModelMgr.GetModel(ModelNames.User) as UserModel;
            if (userModel != null)
            {
                var list = new List<string>();
                list.Add("username=\"" + username + "\"");
                list.Add("password=\"" + password + "\"");

                //uid = userModel.ExistUser(list);
            }
            if (uid > 0L)
            {
                json["result"] = (int)ResultCode.Success;
                json["uid"] = uid;
                InitUser(uid, socket, userModel);
            }
            else
            {
                json["result"] = (int)ResultCode.Failed;
            }
            var retMssage = new WebSocketManager.Message()
            {
                CommandId = Protocal.Login,
                MessageType = MessageType.Json,
                Data = JsonMapper.ToJson(json)
            };
            await handler.SendMessageAsync(socket, retMssage);
            logger.Info(username + " " + password);
        }

        private void InitUser(long uid, WebSocket socket, UserModel model)
        {
            if (handler == null || socket == null)
            {
                throw new Exception("InitUser null!~");
            }
            var socketid = handler.connManager.GetId(socket);
            if (socketid > 0)
            {
                var user = UserMgr.AddUser(socketid, socket);
                user.uid = uid; 
                user.name = model.GetUserName(uid.ToString());
            }
        }
    }
}
