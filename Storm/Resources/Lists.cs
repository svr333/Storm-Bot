using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;
using Storm.Core;
using Storm.Handlers;

namespace Storm.Resources
{
    public static class Lists
    {
        public static readonly IList<string> SansQuote = new List<string>
        {
            "You're gonna have a bad time...",
            "You ever heard of a talking flower?",
            "Hey... puzzles might be fun. If you tried them.",
            "... You'd be dead where you stand.",
            "It's a beautiful day outside. Birds are singing, flowers are blooming... On days like these, kids like you... should be burning in hell.",
            "You are one day closer to eating your next plate of nachos.",
            "err err errrr.",
        }.AsReadOnly();

        public static readonly IList<string> FunFact = new List<string>
        {
            "Fires usually travel much faster uphill than downhill.",
            "In 1967, Australia's Prime Minister, Harold Holt decided to go swimming and was never seen again. They later named a swimming centre after him.",
            "Spiders can get high and build different kinds of webs while on weed, caffeine, mescaline and LSD.",
            "Elvis Presley was a karate black belt.",
            "Queen Victoria survived at least 7 assassination attempts.",
            "111 111 111 × 111 111 111 = 12 345 678 987 654 321",
            "A 1c coin is worth $5 368 709.12 on day 30 if its value doubled every day.",
            "If a Google employee dies, their spouse receives 50% of their annual salary for 10 years.",
            "Leonardo da Vinci referred to robots as 'mechanical knights'.",
            "In Switzerland it is illegal to own only one guinea pig.",
            "King Henry VIII always slept with a gigantic axe beside him.",
            "The inventor of the frisbee was cremated and made into a frisbee when he died.",
            "There is a species of spider called the Hobo Spider.",
            "'Do geese see god?' is the same when read backwards.",
            "Honeybees can recognize human faces.",
            "The bird in the Twitter logo has a name - Larry.",
            "Birds do not urinate.",
            "There is an Official Wizard of New Zealand. His name is Ian Brackenbury Channell.",
            "Skittles are infinitely better than M&Ms.",
        }.AsReadOnly();

        public static readonly IList<Color> Colors = new List<Color>
        {
            Color.Blue,
            Color.Gold,
            Color.Green,
            Color.Magenta,
            Color.Orange,
            Color.Purple,
            Color.Red,
            Color.Teal
        }.AsReadOnly();

        public static readonly IList<string> BotStatus = new List<string>
        {
            $"Made by Tempest#0003 | {Config.bot.cmdPrefix}help",
            $"https://github.com/tomwmth | {Config.bot.cmdPrefix}help",
            $"https://discord.gg/G2m33CV | {Config.bot.cmdPrefix}help"
        }.AsReadOnly();

        public static readonly IList<string> Dice = new List<string>
        {
            $":one:",
            $":two:",
            $":three:",
            $":four:",
            $":five:",
            $":six:"
        }.AsReadOnly();

        public const uint DailyCoinsGain = 250;
        public const int MessageRewardCooldown = 10;
        public const int MessageRewardMinLength = 5;
        public const int XpPerMessageGain = 10;
        public static readonly Tuple<int, int> MessageRewardMinMax = Tuple.Create(5, 10);

        //Exception Messages
        public static readonly string ExDailyTooSoon = "You've already collected your daily for today.";
        public static readonly string ExTransferSameUser = "Cannot transfer coins to the same user.";
        public static readonly string ExTransferToBot = "You cannot transfer coins to a bot.";
        public static readonly string ExTransferNotEnoughFunds = "You do not have enough coins to transfer.";
    }
    public static class ChooseRandom
    {
        private static Random rng = new Random();

        public static T RandomElement<T>(this IList<T> list)
        {
            return list[rng.Next(list.Count)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[rng.Next(array.Length)];
        }
    }
}
