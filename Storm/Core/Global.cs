using System;
using System.Collections.Generic;
using Discord.WebSocket;
using Storm.Helpers;
using Storm.Resources;

namespace Storm.Core
{
    internal static class Global
    {
        internal static DiscordSocketClient Client { get; set; }

        internal static Dictionary<ulong, string> MessagesIdToTrack { get; set; }

        internal static bool Headless = false;

        internal static Random Rnd { get; set; } = new Random();

        internal static List<HelpMessage> HelpMessagesToTrack { get; set; } = new List<HelpMessage>();

        internal static void WriteColoredLine(string text, ConsoleColor color, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        internal static string GetRandomSansQuote()
        {
            return Lists.SansQuote[Rnd.Next(0, Lists.SansQuote.Count)];
        }
    }
}
