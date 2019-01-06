using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Discord;
using System.Linq;
using Storm.Core;
using Storm.Helpers;
using Storm.Resources;

namespace Storm.Handlers
{
    internal class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _cmdService;
        private IServiceProvider _serviceProvider;

        public CommandHandler(DiscordSocketClient client, CommandService cmd, IServiceProvider serv)
        {
            _client = client;
            _cmdService = cmd;
            IServiceProvider = serv;
        }
        
        
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            //you dont need client in here anymore
            var cmdConfig = new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            };
            await _cmdService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
            await _client.SetGameAsync(Lists.BotStatus.RandomElement());
            _client.MessageReceived += HandleCommandAsync;
        }

        internal async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos)
                 || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _cmdService.ExecuteAsync(context, argPos, _serviceProvider);
                if (result.IsSuccess)
                {
                    return;
                }
                if (result.Error == CommandError.BadArgCount)
                {
                    var searchResult = _cmdService.Search(context, argPos);
                    var commandInfo = searchResult.Commands.FirstOrDefault().Command;
                    var commandInfoEmbed = EmbedHelper.CreateEmbed(commandInfo);
                    await context.Channel.SendMessageAsync("", false, commandInfoEmbed);
                }
            }
        }
    }
}
