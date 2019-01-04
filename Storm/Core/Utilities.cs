using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Storm.Core
{
    class Utilities
    {
        private static Dictionary<string, string> config;

        static Utilities()
        {
            string json = File.ReadAllText("Resources/config.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            config = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetConfig(string status)
        {
            if (config.ContainsKey(status)) return config[status];
            return "";
        }
    }
}
