
namespace WebSocketManager
{
    /// <summary>
    /// 网络协议文件
    /// </summary>
    public enum Protocal : ushort
    {
        Default = 1000,                 //缺省消息
        Connect = 1002,                 //连接消息
        Disconnect = 1003,              //异常掉线
        Register = 1004,                //序列化字段
        Login = 1005,                   //用户登录
        ReqUserInfo = 1006,             //请求用户信息
        Battle = 1007,                  //请求战斗
        ReqLobbyList = 1008,            //请求大厅列表
        ReqLobbyEnter = 1009,           //请求大厅信息
        ReqMate = 1010,                 //请求匹配
        ReqRoomInfo = 1011,             //请求房间信息
        ReqRoomEnter = 1012,            //请求进入房间
        Logout = 1013,                  //退出游戏
        GameStart = 1014,               //游戏开始
        GameOver = 1015,                //游戏结束
        ReqExitLobby = 1016,            //退出大厅
        ReqExitRoom = 1017,             //退出房间
    }

    public enum NpcType : ushort
    {
        Player = 0,
        Monster = 1,
    }

    /// <summary>
    /// 结果码
    /// </summary>
    public enum ResultCode : ushort
    {
        Success = 0,             //操作成功
        Failed = 2001,              //操作失败
        ExistUser = 2002,           //用户已注册
    }
}