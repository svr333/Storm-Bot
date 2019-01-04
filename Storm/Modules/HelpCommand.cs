using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Storm.Core;
using Storm.Helpers;

namespace Storm.Modules
{
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Alias("h")]
        [Summary("An interactive command showing all possible commands and their syntax.")]
        public async Task ExecuteHelpAsync()
        {
            var avatar = Context.User.GetAvatarUrl();
            var footer = new EmbedFooterBuilder()
                .WithText($"Requested by {Context.User.Username}#{Context.User.Discriminator}")
                .WithIconUrl(avatar);
            var eb = new EmbedBuilder();
            eb.WithTitle(":package: Please select a category:");
            eb.AddField(":hammer: Moderation", "Commands related to server moderation.");
            eb.AddField(":money_with_wings: Economy", "Commands to utilize the bot's economy features.", true);
            eb.AddField(":open_file_folder: Miscellaneous", "Various uncategorized commands.");
            eb.AddField(":pencil: Math", "ill do this later lmao these summaries gay", true);
            eb.WithDescription("*React to this message with the emoji of the module you would like to view!*");
            eb.WithColor(Color.Blue);
            eb.WithFooter(footer);
            eb.WithCurrentTimestamp();
            var msg = await Context.Channel.SendMessageAsync("", false, eb.Build());
            var rmsg = (RestUserMessage) await msg.Channel.GetMessageAsync(msg.Id);
            var folderEmoji = new Emoji("\uD83D\uDCC2");
            var hammerEmoji = new Emoji("\uD83D\uDD28");
            var moneyEmoji = new Emoji("\uD83D\uDCB8");
            var pencilEmoji = new Emoji("\uD83D\uDCDD");
            var cancelEmoji = new Emoji("\u26D4");
            await rmsg.AddReactionAsync(hammerEmoji, new RequestOptions());
            await rmsg.AddReactionAsync(moneyEmoji, new RequestOptions());
            await rmsg.AddReactionAsync(folderEmoji, new RequestOptions());
            await rmsg.AddReactionAsync(pencilEmoji, new RequestOptions());
            await rmsg.AddReactionAsync(cancelEmoji, new RequestOptions());
            var helpMessage = new HelpMessage
            {
                MessageId = msg.Id,
                GuildId = Context.Guild.Id
            };
            Global.HelpMessagesToTrack.Add(helpMessage);
        }
    }
}
