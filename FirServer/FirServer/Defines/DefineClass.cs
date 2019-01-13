using System.Collections.Generic;

namespace FirServer.Defines
{
    public class GlobalConfig
    {
        public string name { get; set; }
        public Percent percent { get; set; }
        public Percent takecashPoundage { get; set; }
        public float failAmount { get; set; }

        public List<LobbyData> lobbyList = new List<LobbyData>();
        public List<RechargeData> rechargeList = new List<RechargeData>();
    }

    public class Percent
    {
        public float baseValue;
        public float maxValue;
    }

    public class LobbyData
    {
        public int id;
        public string name;
        public int maxUserCount;
        public int maxRoomCount;
    }

    public class RechargeData
    {
        public int id;
        public string name;
    }

    public class FishData
    {
        public ushort id;
        public int type;
        public int health;
    }

    public class FishInfo
    {
        public long id;
        public int type;
        public int health;
    }

    public class BulletData
    {
        public ushort id;
        public int type;
        public int damage;
    }
}

