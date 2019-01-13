using System.Collections;
using Mono.Xml;
using System.Security;
using System.IO;

namespace Utility
{
    public class XmlHelper
    {
        private static string currDir = null;
        static string CurrDirPath
        {
            get
            {
                if (currDir == null)
                {
                    var path = Directory.GetCurrentDirectory();
                    currDir = path.Replace('\\', '/');
                }
                return currDir;
            }
        }

        public static SecurityElement LoadXml(string xmlPath)
        {
            xmlPath = CurrDirPath + "/" + xmlPath;
            if (!xmlPath.EndsWith(".xml"))
            {
                xmlPath += ".xml";
            }
            var sp = new SecurityParser();
            var data = File.ReadAllText(xmlPath);
            sp.LoadXml(data.ToString());
            return sp.ToXml();
        }
    }
}
