using LitJson;
using WebSocketManager;

namespace FirServer.Controllers
{
    public class ResultData
    {
        public ResultData()
        {
            data = string.Empty;
            errcode = (ushort)ResultCode.Failed;
        }

        public ResultData(ResultCode result, JsonData data)
        {
            this.errcode = (ushort)result;
            this.data = JsonMapper.ToJson(data);
        }

        public ushort errcode { get; set; }
        public string data { get; set; }

        public override string ToString()
        {
            return "errcode:" + errcode + " data:" + data;
        }
    }
}
