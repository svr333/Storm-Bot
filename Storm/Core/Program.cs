using Discord;
using Discord.WebSocket;
using Storm.Handlers;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Storm.Core
{
    internal class Program
    {
        private DiscordSocketClient _client;
        private IServiceProvider _services;
        private CommandHandler _handler;

        private static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (string.IsNullOrEmpty(Config.bot.token))
            {
                Global.WriteColoredLine("CRITICAL ERROR: There is no token configured!", ConsoleColor.Red);
                return;
            }
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();

            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_handler)
                .AddSingleton<CommandService>()
                .BuildServiceProvider();
        }

        private static Task Log(LogMessage msg)
        {
            Global.WriteColoredLine(msg.Message, ConsoleColor.Green);
            return Task.CompletedTask;
        }
    }
}
