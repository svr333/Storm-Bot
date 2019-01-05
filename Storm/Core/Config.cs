using Newtonsoft.Json;
using System.IO;

namespace Storm.Core
{
    internal class Config
    {
        //private const string configFolder = "Resources";
        //private const string configFile = "config.json";

        public static BotConfig bot;

        static Config()
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");
            if (!File.Exists("Resources/config.json"))
            {
                bot = new BotConfig();
                var json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText("Resources/config.json", json);
            }
            else
            {
                string json = File.ReadAllText("Resources/config.json");
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }

    public struct BotConfig
    {
        public string token;
        public string cmdPrefix;
        public string version;
        public string checkEmote;
        public string crossEmote;
        public string coinEmote;
        public string weatherApiKey;
        public ulong botOwnerId;
    }
}
