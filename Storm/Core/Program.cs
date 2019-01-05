using Discord;
using Discord.WebSocket;
using Storm.Handlers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Storm.Resources;
using EventHandler = Storm.Handlers.EventHandler;

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
                Console.WriteLine("The bot's token is missing!");
                return;
            }
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            _client.Ready += InternalTimer.StartTimer;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            Global.Client = _client;
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

#pragma warning disable CS1998 
        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
