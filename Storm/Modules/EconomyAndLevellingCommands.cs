using Discord.Commands;
using Discord;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Storm.Resources;
using Storm.Accounts;
using Storm.Core;
using Storm.Helpers.Economy;

namespace Storm.Modules
{
    public class EconomyAndLevellingCommands : ModuleBase<SocketCommandContext>
    {

        [Command("profile")]
        [Summary("View a user's profile.")]
        public async Task GetProfileAsync()
        {
            var account = UserAccounts.GetAccount(Context.User);
            var nl = Environment.NewLine;
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder();
            eb.WithTitle($"{Context.User.Username}#{Context.User.Discriminator}'s Profile");
            eb.WithDescription($":gem: **Level:** {account.LevelNumber}{nl}{nl}:flower_playing_cards: **XP:** {account.XP}{nl}{nl}<:DawnCoin:526673931797790720> **Coins:** {account.Coins}");
            eb.WithColor(Lists.colorsArray[RandomizeColor]);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        [Command("profile")]
        [Summary("View a user's profile.")]
        public async Task GetProfileAsync(SocketUser user)
        {
            var account = UserAccounts.GetAccount(user);
            var nl = Environment.NewLine;
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder();
            eb.WithTitle($"{user.Username}#{user.Discriminator}'s Profile");
            eb.WithDescription($":gem: **Level:** {account.LevelNumber}{nl}{nl}:flower_playing_cards: **XP:** {account.XP}{nl}{nl}<:DawnCoin:526673931797790720> **Coins:** {account.Coins}");
            eb.WithColor(Lists.colorsArray[RandomizeColor]);
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        [Command("addcoins")]
        public async Task AddCoins(uint amount)
        {
            if (Context.User.Id != Config.bot.botOwnerId)
            {
                await ReplyAsync("<:DawnCross:512265417247424512> You must be the bot owner to execute this command!");
            }
            else
            {
                var account = UserAccounts.GetAccount(Context.User);
                account.Coins += amount;
                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"<:DawnCoin:526673931797790720> {amount} have been added to your wallet!");
            }
        }

        //private readonly IDailyCoins dailyCoins;

        //public EconomyAndLevellingCommands(IDailyCoins dailyCoins, ICoinsTransfer coinsTransfer)
        //{
        //    this.dailyCoins = dailyCoins;
        //    this.coinsTransfer = coinsTransfer;
        //}

        //[Command("daily")]
        //[Summary("Recieve a small amount of coins each day.")]
        //public async Task GetDailyAsync()
        //{
        //    try
        //    {
        //        dailyCoins.GetDaily(Context.User.Id);
        //        await ReplyAsync($"Here's {Lists.DailyCoinsGain} miunies, {Context.User.Mention}! Just for you...");
        //    }
        //    catch (InvalidOperationException e)
        //    {
        //        var timeSpanString = string.Format("{0:%h} hours {0:%m} minutes {0:%s} seconds", new TimeSpan(24, 0, 0).Subtract((TimeSpan)e.Data["sinceLastDaily"]));
        //        await ReplyAsync($"You've already collected your daily for today, {Context.User.Mention}!.\nCome back in {timeSpanString}.");
        //    }
        //}
    }
}
