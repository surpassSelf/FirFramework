using System;
using System.Collections.Generic;
using System.Data;

namespace FirServer.Models
{
    public class BaseModel : BaseBehaviour
    {
        protected string tableName;

        public string TableName
        {
            get { return tableName; }
        }

        public BaseModel(string tabName)
        {
            this.tableName = tabName;
        }

        protected string Get(string uid, string strKey)
        {
            if (string.IsNullOrEmpty(tableName) || DataMgr == null)
            {
                throw new Exception();
            }
            return DataMgr.Get(tableName, uid, strKey);
        }

        protected void Set(string uid, string strKey, string value)
        {
            if (string.IsNullOrEmpty(tableName) || DataMgr == null)
            {
                throw new Exception();
            }
            DataMgr.Set(tableName, uid, strKey, value);
        }

        protected long Add(List<string> values)
        {
            if (string.IsNullOrEmpty(tableName) || DataMgr == null)
            {
                throw new Exception();
            }
            return DataMgr.Add(tableName, values);
        }

        public DataSet Query(List<string> values = null)
        {
            if (string.IsNullOrEmpty(tableName) || DataMgr == null)
            {
                throw new Exception();
            }
            return DataMgr.Query(tableName, values);
        }

        public DataSet Exist(string uid)
        {
            if (string.IsNullOrEmpty(tableName) || DataMgr == null)
            {
                throw new Exception();
            }
            return DataMgr.Exist(tableName, uid);
        }
    }
}
