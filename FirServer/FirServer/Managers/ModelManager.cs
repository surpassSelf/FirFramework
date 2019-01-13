using System;
using System.Collections.Generic;
using log4net;
using FirServer.Defines;
using FirServer.Models;

namespace FirServer.Managers
{
    public class ModelManager : BaseBehaviour, IManager
    {
        private static Dictionary<string, BaseModel> models = new Dictionary<string, BaseModel>();
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(ModelManager));

        public void Initialize()
        {
            ModelMgr = this;

            models.Clear();
            AddModel(ModelNames.User, new UserModel());
            AddModel(ModelNames.Battle, new BattleModel());
        }

        public BaseModel GetModel(string strKey)
        {
            if (models.ContainsKey(strKey))
            {
                return models[strKey];
            }
            return null;
        }

        public void AddModel(string strKey, BaseModel model)
        {
            if (models.ContainsKey(strKey))
            {
                throw new Exception();
            }
            models.Add(strKey, model);
        }

        public void RemoveModel(string strKey)
        {
            if (models.ContainsKey(strKey))
            {
                models.Remove(strKey);
            }
        }

        public void OnDispose()
        {
            ModelMgr = null;
        }
    }
}
