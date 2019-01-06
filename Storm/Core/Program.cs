using Discord;
using Discord.WebSocket;
using Storm.Handlers;
using System;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Storm.Preconditions;
using EventHandler = Storm.Handlers.EventHandler;

namespace Storm.Core
{
    internal class Program
    {
        private DiscordSocketClient _client;
        private IServiceProvider _services;

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

            _services = ConfigureServices();
            _services.GetRequiredService<EventHandler>().InitDiscordEvents();
            await _services.GetRequiredService<CommandHandler>().InitializeAsync(_client);
            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<CommandHandler>()
                .AddSingleton<CommandService>()
                .AddSingleton<EventHandler>()
                .AddSingleton<MessageRewardHandler>()
                .AddSingleton<Cooldown>()
                .AddSingleton<NoSelf>()
                .AddSingleton<RequireBotHierarchy>()
                .BuildServiceProvider();
        }

        private static Task Log(LogMessage msg)
        {
            Global.WriteColoredLine(msg.Message, ConsoleColor.White);
            return Task.CompletedTask;
        }
    }
}
