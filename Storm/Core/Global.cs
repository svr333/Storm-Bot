using System.Collections.Generic;
using Discord.WebSocket;
using Storm.Modules;
using Storm.Helpers;

namespace Storm.Core
{
    internal static class Global
    {
        internal static DiscordSocketClient Client { get; set; }
        internal static ulong MessageIdToTrack { get; set; }
        internal static List<HelpMessage> HelpMessagesToTrack { get; set; } = new List<HelpMessage>();
    }
}
