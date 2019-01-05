using System;
using System.ComponentModel.Design;
using System.Linq;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using Storm.Accounts;
using Storm.Core;
using Storm.Resources;

namespace Storm.Modules
{
    public class ModCommands : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [Summary("Kick a user from the server.")]
        public async Task KickUser(IGuildUser user, [Remainder]string reason = "no reason provided")
        {
            await user.GetOrCreateDMChannelAsync();
            var kickdmembed = new EmbedBuilder();
            kickdmembed.WithTitle($"{Config.bot.crossEmote}  Kicked from {Context.Guild.Name}");
            kickdmembed.AddField($"Moderator:", Context.User.Username + Context.User.Discriminator);
            kickdmembed.AddField($"Reason:", reason);
            kickdmembed.WithCurrentTimestamp();
            kickdmembed.WithColor(Color.Red);
            await user.SendMessageAsync("", false, kickdmembed.Build());
            await user.KickAsync(reason);
            await Context.Channel.SendMessageAsync($"**{Config.bot.checkEmote} {user.Username} was kicked for '{reason}'.**");
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Summary("Ban a user from the server.")]
        public async Task BanUser(IGuildUser user, [Remainder]string reason = "no reason provided")
        {
            await user.GetOrCreateDMChannelAsync();
            var bandmembed = new EmbedBuilder();
            bandmembed.WithTitle($"{Config.bot.crossEmote}  Banned from {Context.Guild.Name}");
            bandmembed.AddField($"Moderator:", Context.User.Username + Context.User.Discriminator);
            bandmembed.AddField($"Reason:", reason);
            bandmembed.WithCurrentTimestamp();
            bandmembed.WithColor(Color.Red);
            await user.SendMessageAsync("", false, bandmembed.Build());
            await user.Guild.AddBanAsync(user, 0, reason);
            await Context.Channel.SendMessageAsync($"**{Config.bot.checkEmote} {user.Username} was banned for '{reason}'.**");
        }

        [Command("warn")]
        [Summary("Warns a user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task WarnUser(IGuildUser user, [Remainder]string reason = "no reason provided")
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            var eb = new EmbedBuilder();
            var nl = Environment.NewLine;
            var randomizeColor = new Random().Next(Lists.colorsArray.Length);
            eb.WithDescription($"**{user.Username}** has been warned.{nl}**Reason:** {reason}{nl}**Warning Number** {userAccount.NumberOfWarnings}{nl}**Moderator:** {Context.User.Username}");
            eb.WithColor(Lists.colorsArray[randomizeColor]);
            await ReplyAsync("", false, eb.Build());

            await user.GetOrCreateDMChannelAsync();
            var eb2 = new EmbedBuilder();
            eb2.WithDescription($"You have been warned in *{Context.Guild.Name}*.{nl}**Reason:** {reason}{nl}**Warning Number** {userAccount.NumberOfWarnings}{nl}**Moderator:** {Context.User.Username}");
            eb2.WithColor(Lists.colorsArray[randomizeColor]);
            await user.SendMessageAsync("", false, eb2.Build());
        }

        [Command("warnings")]
        [Summary("Check a user's warnings.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task CheckWarnings(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            var eb = new EmbedBuilder();
            var randomizeColor = new Random().Next(Lists.colorsArray.Length);
            eb.WithDescription($"**{user.Username}** has {userAccount.NumberOfWarnings} warnings.");
            eb.WithColor(Lists.colorsArray[randomizeColor]);
            await ReplyAsync("", false, eb.Build());
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Summary("Unban a user from the server.")]
        public async Task UnbanUser(ulong userId)
        {
            await Context.Guild.RemoveBanAsync(userId);
            await Context.Channel.SendMessageAsync($"**{Config.bot.checkEmote} User was unbanned**.");
        }

        [Command("softban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Summary("Softban a user from the server.")]
        public async Task SoftbanUser(IGuildUser user, [Remainder]string reason = "no reason provided")
        {
            await user.GetOrCreateDMChannelAsync();
            var softbandmembed = new EmbedBuilder();
            softbandmembed.WithTitle($"{Config.bot.crossEmote}  Banned from {Context.Guild.Name}");
            softbandmembed.AddField($"Moderator:", Context.User.Username + Context.User.Discriminator);
            softbandmembed.AddField($"Reason:", reason);
            softbandmembed.WithCurrentTimestamp();
            softbandmembed.WithColor(Color.Red);
            await user.SendMessageAsync("", false, softbandmembed.Build());
            await user.Guild.AddBanAsync(user, 7, reason);
            await Context.Channel.SendMessageAsync($"**{Config.bot.checkEmote} {user.Username} was softbanned for '{reason}'.**");
            await user.Guild.RemoveBanAsync(user);
        }

        [Command("purge")]
        [Alias("prune", "clear")]
        [Summary("Mass delete messages in a channel.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task PurgeMessages(int messagesToDelete)
        {
            if (messagesToDelete > 100)
            {
               await ReplyAsync($"{Config.bot.crossEmote} Sorry, you cannot purge more than 100 messages at a time.");
            }
            else
            {
                var channel = (ITextChannel)Context.Channel;
                var messages = (await channel.GetMessagesAsync(messagesToDelete + 1).FlattenAsync());
                await channel.DeleteMessagesAsync(messages);
                const int delay = 2000;
                var purgeMessageDelay = await ReplyAsync($"{Config.bot.checkEmote} Purge completed. ***This message will be deleted in {delay / 1000} seconds.***");
                await Task.Delay(delay);
               await purgeMessageDelay.DeleteAsync();
            }
        }

        [Command("serverinfo")]
        [Alias("server", "info")]
        [Summary("View server information.")]
        public async Task ServerInfo()
        {
            var eb = new EmbedBuilder();
            var randomizeColor = new Random().Next(Lists.colorsArray.Length);
            var serverOwner = Context.Guild.Owner;
            var members = Context.Guild.MemberCount;
            var region = Context.Guild.VoiceRegionId;
            var serverIcon = Context.Guild.IconUrl;

            eb.WithTitle("Server Information " + $"*{Context.Guild.Name}*");
            eb.AddField("Server Owner", serverOwner);
            eb.AddField("Members", members);
            eb.AddField("Server Region", region);
            eb.WithFooter("Made by Tempest#0003");
            eb.WithThumbnailUrl(serverIcon);
            eb.WithCurrentTimestamp();
            eb.WithColor(Lists.colorsArray[randomizeColor]);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        [Command("botinfo")]
        [Summary("View bot information.")]
        public async Task BotInfo()
        {
            if (Context.User.Id != Config.bot.botOwnerId)
            {
                await ReplyAsync($"{Config.bot.crossEmote} You must be the bot owner to execute this command!");
            }
            else
            {
                var eb = new EmbedBuilder();
                var randomizeColor = new Random().Next(Lists.colorsArray.Length);

                eb.WithTitle("Bot Information");
                eb.AddField("Guilds", Global.Client.Guilds.Count);
                eb.AddField("Shard", Global.Client.ShardId);
                eb.AddField("Latency", Global.Client.Latency);
                eb.WithColor(Lists.colorsArray[randomizeColor]);
                eb.WithThumbnailUrl("https://avatars1.githubusercontent.com/u/44721791?s=460&v=4");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
    }
}
