using System;
using StackExchange.Redis;
using FirServer.Defines;

namespace FirServer.Utility
{
    public class RedisUtility
    {
        private static ConnectionMultiplexer redis;
        private static IDatabase db = null;

        public static void Initialize()
        {
            if (AppConst.RedisMode)
            {
                ConfigurationOptions option = new ConfigurationOptions
                {
                    Password = "jar510@Lee",
                    AbortOnConnectFail = false,
                    EndPoints = { { "127.0.0.1", 6379 } }
                };
                redis = ConnectionMultiplexer.Connect(option);
                db = redis.GetDatabase();
            }
        }

        public static bool StringSet(string key, string value, int seconds)
        {
            if (db == null)
            {
                return false;
            }
            return db.StringSet(key, value, TimeSpan.FromSeconds(seconds));
        }


        public static string StringGet(string key)
        {
            if (db == null)
            {
                return null;
            }
            return db.StringGet(key);
        }

        public static bool KeyDelete(string key)
        {
            if (db == null)
            {
                return false;
            }
            return db.KeyDelete(key);
        }

        public static bool KeyExist(string key)
        {
            if (db == null)
            {
                return false;
            }
            return db.KeyExists(key);
        }

        public static void Close()
        {
            redis.Close();
        }
    }
}
