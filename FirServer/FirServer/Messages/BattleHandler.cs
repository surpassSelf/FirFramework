using System;
using System.Net.WebSockets;
using LitJson;
using WebSocketManager;

namespace FirServer.Messages
{
    /// <summary>
    /// 战斗处理器
    /// </summary>
    public class BattleHandler : BaseMessageHandler
    {
        public override async void OnMessage(WebSocket socket, WebSocketHandler handler, JsonData message)
        {
            var json = new JsonData();
            var uid = long.Parse(message["uid"].ToString());
            var seed = 10;

            var retMssage = new WebSocketManager.Message()
            {
                CommandId = Protocal.Battle,
                MessageType = MessageType.Json,
                Data = JsonMapper.ToJson(json)
            };
            await handler.SendMessageAsync(socket, retMssage);
        }
    }
}
