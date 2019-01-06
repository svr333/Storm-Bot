using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Storm.Core;

namespace Storm.Preconditions
{
    public class RequireBotHierarchy : ParameterPreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, ParameterInfo parameter, object value, IServiceProvider services)
        {
            var user = value is SocketGuildUser ? (SocketGuildUser)value : null;

            var bot = (context.Guild as SocketGuild).GetUser(context.Client.CurrentUser.Id);
            if ((user != null) && (bot.Hierarchy < user.Hierarchy))
                return Task.FromResult(PreconditionResult.FromError($"{Config.bot.crossEmote} Command cannot be performed on user with a role higher than the bot."));

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
