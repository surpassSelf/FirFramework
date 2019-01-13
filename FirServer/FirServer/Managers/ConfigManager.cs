using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Utility;
using FirServer.Defines;

namespace FirServer.Managers
{
    public class ConfigManager : BaseBehaviour, IManager
    {
        private static GlobalConfig globalConfig = new GlobalConfig();

        public void Initialize()
        {
            ConfigMgr = this;
            LoadGlobalConfig();
        }

        void LoadGlobalConfig()
        {
            var xml = XmlHelper.LoadXml("Config/config.xml");
            if (xml != null)
            {
                var count = xml.Children.Count;
                for(int i = 0; i < count; i++)
                {
                    var node = xml.Children[i] as SecurityElement;
                    if (node != null)
                    {
                        switch (node.Tag)
                        {
                            case "global": ParseGlobal(node); break;
                            case "lobbys": ParseLobbys(node); break;
                            case "recharges": ParseRecharges(node); break;
                        }
                    }
                }
            }
        }

        public GlobalConfig GetGlobalConfig()
        {
            return globalConfig;
        }

        /// <summary>
        /// 分析全局属性
        /// </summary>
        /// <param name="node"></param>
        void ParseGlobal(SecurityElement node)
        {
            if (node != null)
            {
                globalConfig.name = node.Attributes["name"].ToString();
                globalConfig.percent = GetPercent(node.Attributes["percent"].ToString());
                globalConfig.takecashPoundage = GetPercent(node.Attributes["takecashPoundage"].ToString());
                globalConfig.failAmount = float.Parse(node.Attributes["failAmount"].ToString());
            }
        }

        Percent GetPercent(string str)
        {
            Percent obj = null;
            if (!string.IsNullOrEmpty(str))
            {
                obj = new Percent();
                var strs = str.Split(':');
                obj.baseValue = float.Parse(strs[0]);
                obj.maxValue = float.Parse(strs[1]);
            }
            return obj;
        }

        /// <summary>
        /// 分析大厅属性
        /// </summary>
        /// <param name="node"></param>
        private void ParseLobbys(SecurityElement node)
        {
            if (node != null)
            {
                var nodes = node.Children;
                for(int i = 0; i < nodes.Count; i++)
                {
                    var obj = nodes[i] as SecurityElement;
                    var lobby = new LobbyData();
                    lobby.id = int.Parse(obj.Attribute("id"));
                    lobby.name = obj.Attribute("name");
                    lobby.maxUserCount = int.Parse(obj.Attribute("maxUserCount"));
                    lobby.maxRoomCount = int.Parse(obj.Attribute("maxRoomCount"));
                    globalConfig.lobbyList.Add(lobby);
                }
            }
        }

        /// <summary>
        /// 分析
        /// </summary>
        /// <param name="node"></param>
        private void ParseRecharges(SecurityElement node)
        {
            if (node != null)
            {
                var nodes = node.Children;
                for (int i = 0; i < nodes.Count; i++)
                {
                    var obj = nodes[i] as SecurityElement;
                    var lobby = new RechargeData();
                    lobby.id = int.Parse(obj.Attribute("id"));
                    lobby.name = obj.Attribute("name");
                    globalConfig.rechargeList.Add(lobby);
                }
            }
        }

        public void OnDispose()
        {
            ConfigMgr = null;
        }
    }
}
