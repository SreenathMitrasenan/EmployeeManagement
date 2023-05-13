using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAutomation.Utilities
{
    class RuntimeDataSaver
    {
        public Dictionary<string , string> oDict = new Dictionary<string , string>();

        private RuntimeDataSaver() { }
        private static RuntimeDataSaver _instance;
        private static RuntimeDataSaver GetInstance()
        {
            if (_instance == null) _instance = new RuntimeDataSaver();
            return _instance;
        }

        public void Set(string id, string val)
        {
            oDict.Add(id, val);
        }
        public string Get(string id)
        {
            return oDict[id].ToString();
        }
    }
}
