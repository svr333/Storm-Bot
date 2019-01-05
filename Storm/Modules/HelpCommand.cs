using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.CommandsExtension;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Storm.Core;
using Storm.Helpers;

namespace Storm.Modules
{
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;

        public HelpCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        [Command("help")]
        [Summary("Presents a list of commands.")]
        public async Task Help([Remainder] string command = null)
        {
            const string botPrefix = "-";
            var helpEmbed = _commandService.GetDefaultHelpEmbed(command, botPrefix);
            await Context.Channel.SendMessageAsync(embed: helpEmbed);
        }
    }
}