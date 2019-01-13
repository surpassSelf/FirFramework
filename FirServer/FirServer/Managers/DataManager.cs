using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using log4net;
using MySql.Data.MySqlClient;
using FirServer.Utility;

namespace FirServer.Managers
{
    public class DataManager : BaseBehaviour, IManager
    {
        const int expireTime = 86400 * 3;
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(DataManager));

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            DataMgr = this;

            RedisUtility.Initialize();
            MysqlUtility.Initialize();
        }

        public MySqlDbType GetDbType(string typestr)
        {
            switch(typestr)
            {
                case "int": return MySqlDbType.Int32;
                case "str": return MySqlDbType.VarChar;
                default: return MySqlDbType.VarChar;
            }
        }

        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long Add(string tabName, List<string> values)
        {
            string valuestr = string.Empty;
            var sqlParams = new MySqlParameter[values.Count];
            for(int i = 0; i < values.Count; i++)
            {
                var strKey = "@value" + i;
                var strs = values[i].Split(':');
                var dbType = GetDbType(strs[0]);
                sqlParams[i] = new MySqlParameter(strKey, dbType) { Value = strs[1] };

                if (!string.IsNullOrEmpty(valuestr))
                {
                    valuestr += ", ";
                }
                valuestr += strKey;
            }
            string strsql = "insert into " + tabName + " values (" + valuestr + ");";
            return MysqlUtility.ExecuteSql(strsql, sqlParams);
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="uid"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string tabName, string uid, string key, string value)
        {
            var strs = value.Split(':');
            var dbType = GetDbType(strs[0]);
            var sqlParams = new MySqlParameter[]
            {
                new MySqlParameter("@value", dbType) { Value = strs[1] },
                new MySqlParameter("@openid", MySqlDbType.VarChar) { Value = uid },
            };
            string strKey = tabName + "_" + uid + "_" + key;
            RedisUtility.StringSet(strKey, strs[1], expireTime);

            var strsql = "update " + tabName + " set " + key + "=@value where openid=@openid";
            MysqlUtility.ExecuteSql(strsql, sqlParams);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="uid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string tabName, string uid, string key)
        {
            string strKey = tabName + "_" + uid + "_" + key;
            if (RedisUtility.KeyExist(strKey))
            {
                return RedisUtility.StringGet(strKey);
            }
            var sqlParams = new MySqlParameter[]
            {
                new MySqlParameter("@openid", MySqlDbType.VarChar) { Value = uid },
            };
            var strsql = "select " + key + " from " + tabName + " where openid=@openid";
            var dataset = MysqlUtility.ExecuteQuery(strsql, sqlParams);
            var obj = dataset.Tables[0].Rows[0][key].ToString();
            if (obj != null)
            {
                logger.Warn("strKey:  " + strKey + " obj: " + obj);
                RedisUtility.StringSet(strKey, obj, expireTime);
            }
            return obj;
        }

        /// <summary>
        /// 组合查询   Key:Type:Value    nick:str:jarjin
        /// </summary>
        /// <returns></returns>
        public DataSet Query(string tabName, List<string> values = null)
        {
            MySqlParameter[] sqlParams = null;
            var strsql = "select * from " + tabName;
            if (values != null)
            {
                string valuestr = string.Empty;
                sqlParams = new MySqlParameter[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    var strs = values[i].Split(':');
                    var valKey = "@value" + i;
                    var dbType = GetDbType(strs[1]);
                    sqlParams[i] = new MySqlParameter(valKey, dbType) { Value = strs[2] };

                    if (!string.IsNullOrEmpty(valuestr))
                    {
                        valuestr += ", ";
                    }
                    valuestr += strs[0] + "=" + valKey;
                }
                strsql += " where " + valuestr;
            }
            DataSet dataset = null;
            try
            {
                dataset = MysqlUtility.ExecuteQuery(strsql, sqlParams);
            }
            finally
            {
                logger.Info("strsql:" + strsql);
            }
            return dataset;
        }

        /// <summary>
        /// 存在一条记录
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataSet Exist(string tabName, string uid)
        {
            var sqlParams = new MySqlParameter[]
            {
                new MySqlParameter("@openid", MySqlDbType.VarChar) { Value = uid },
            };
            var strsql = "select * from " + tabName + " where openid=@openid";
            DataSet dataset = null;
            try
            {
                dataset = MysqlUtility.ExecuteQuery(strsql, sqlParams);
            }
            finally
            {
                logger.Info("strsql:" + strsql);
            }
            return dataset;
        }

        /// <summary>
        /// 移除某个字段
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="uid"></param>
        /// <param name="key"></param>
        public void Remove(string tabName, string uid, string key)
        {
            if (RedisUtility.KeyExist(key))
            {
                RedisUtility.KeyDelete(key);
            }
            var sqlParams = new MySqlParameter[]
            {
                new MySqlParameter("@openid", MySqlDbType.VarChar) { Value = uid },
            };
            var strsql = "update " + tabName + " set "+ key + "='' where openid =@openid";
            MysqlUtility.ExecuteSql(strsql, sqlParams);
        }

        /// <summary>
        /// 关闭链接
        /// </summary>
        public void Close()
        {
            MysqlUtility.Close();
            RedisUtility.Close();
        }

        public void OnDispose()
        {
            DataMgr = null;
        }
    }
}
