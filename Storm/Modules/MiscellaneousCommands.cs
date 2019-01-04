using Discord.Commands;
using Discord;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using System.Linq;
using Discord.WebSocket;
using Storm.Core;
using Storm.Resources;

namespace Storm.Modules
{
    public class MiscellaneousCommands : ModuleBase<SocketCommandContext>
    {
        [Command("sans")]
        [Summary("you're gonna have a bad time...")]
        public async Task SansAsync()
        {
            await Context.Channel.SendMessageAsync($"{Context.User.Mention}, {Lists.SansQuote.RandomElement()} <:StormSans:512257021047865354>");
        }

        [Command("funfact")]
        [Alias("fact", "f")]
        [Summary("Provides a random fun fact.")]
        public async Task FunFactAsync()
        {
            await Context.Channel.SendMessageAsync($"**Fun Fact:** {Lists.FunFact.RandomElement()}");
        }

        [Command("liberal")]
        [Alias("libtard", "wreck", "own")]
        [Summary("Own a libtard Ben Shapiro style.")]
        public async Task LiberalAsync(IGuildUser user)
        {
            await user.GetOrCreateDMChannelAsync();
            await user.SendMessageAsync($"{Context.User.Username} just rekt you with facts and knowledge, libtard! :sunglasses:");
            await user.SendMessageAsync($"It's a beautiful day outside. Birds are singing, flowers are blooming... On days like these, __***libtards***__ like you should be burning in hell.");
            await Context.Channel.SendMessageAsync($"**{Config.bot.checkEmote} {Context.User.Username} has owned {user.Username} with facts and knowledge.**");
        }

        [Command("girlfriend")]
        [Alias("gf")]
        public async Task GenerateGirlfriendAsync()
        {
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString($"https://randomuser.me/api/?nat=AU&gender=female");
            }

            var data = JsonConvert.DeserializeObject<dynamic>(json);


            string firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.results[0].name.first.ToString());
            string lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.results[0].name.last.ToString());
            string age = data.results[0].dob.age.ToString();
            string phoneNumber = data.results[0].cell.ToString();
            string avatarURL = data.results[0].picture.large.ToString();
            string street = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.results[0].location.street.ToString());
            string city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.results[0].location.city.ToString());
            string state = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data.results[0].location.state.ToString());

            Console.WriteLine($"RETRIEVED RANDOM PERSON: {firstName} {lastName}");

            var eb = new EmbedBuilder(); ;
            eb.AddField($"Name", $"{firstName} {lastName}");
            eb.AddField($"Address", $"{street}, {city}, {state}");
            eb.AddField($"Phone Number", phoneNumber);
            eb.WithFooter($"Random person generated from randomuser.me");
            eb.WithThumbnailUrl(avatarURL);
            eb.WithCurrentTimestamp();
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            eb.WithColor(Lists.colorsArray[RandomizeColor]);

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

        //[Command("pass")]
        //[Alias("nword", "nwordpass")]
        //public async Task NwordPassAsync()
        //{
        //    Random rnd = new Random(DateTime.Now.Millisecond);
        //    int result = rnd.Next(1, 1001);
        //    Console.WriteLine($"Random Instance created: {result}");
        //    if (result != 876)
        //    {
        //        var eb = new EmbedBuilder();
        //        eb.AddField("You're a fucking wigger.", "N-word pass request denied.");
        //        eb.WithColor(Color.LighterGrey);
        //        eb.WithFooter($"Chance: 1 in 1000.     You rolled {result}.");
        //        eb.WithCurrentTimestamp();
        //        eb.WithThumbnailUrl("http://beta.ems.ladbiblegroup.com/s3/content/26f15c125aecf36ca1702621ef2ddb43.png");
        //        await ReplyAsync("", false, eb);
        //    }
        //    else
        //    {
        //        var eb = new EmbedBuilder();
        //        eb.AddField($"Congratulations, {Context.User.Username}!", "You have now received the N-word pass! You can say nigga!");
        //        eb.WithFooter($"Chance: 1 in 1000.     You rolled {result}.");
        //        eb.WithColor(Color.Gold);
        //        eb.WithCurrentTimestamp();
        //        eb.WithThumbnailUrl("https://i.redd.it/nwkimjc3vwgy.jpg");
        //        await ReplyAsync("", false, eb.Build());
        //    }
        //}

        [Command("rainbow")]
        public async Task RainbowEmbedAsync()
        {
            var eb = new EmbedBuilder();
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            eb.WithColor(Lists.colorsArray[RandomizeColor]);
            eb.WithDescription("This is an embed with a random colour!");
            await ReplyAsync("", false, eb.Build());
        }

        [Command("avatar")]
        [Alias("av")]
        [Summary("Get a user's avatar.")]
        public async Task GetAvatarAsync(SocketUser user)
        {
            var avatar = user.GetAvatarUrl();
            var author = new EmbedAuthorBuilder()
            .WithName($"{user.Username}'s Avatar")
            .WithIconUrl(avatar);
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder()
            .WithColor(Lists.colorsArray[RandomizeColor])
            .WithImageUrl(avatar)
            .WithAuthor(author);
            await ReplyAsync("", false, eb.Build());
        }

        [Command("avatar")]
        [Alias("av")]
        [Summary("Get a user's avatar.")]
        public async Task GetAvatarAsync()
        {
            var avatar = Context.User.GetAvatarUrl();
            var author = new EmbedAuthorBuilder()
                .WithName($"{Context.User.Username}'s Avatar")
                .WithIconUrl(avatar);
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder()
                .WithColor(Lists.colorsArray[RandomizeColor])
                .WithImageUrl(avatar)
                .WithAuthor(author);
            await ReplyAsync("", false, eb.Build());
        }

        [Command("whois")]
        [Alias("who")]
        [Summary("View information for a user.")]
        public async Task WhoIsUserAsync(SocketGuildUser user)
        {
            var avatar = user.GetAvatarUrl();
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder();
            string guildJoinDate = String.Format("{0:d MMMM yyyy} at {0:hh}:{0:mm tt} GMT", user.JoinedAt);
            string accountCreationDate = String.Format("{0:d MMMM yyyy} at {0:hh}:{0:mm tt} GMT", user.CreatedAt);
            var userRoles = user.Roles.Select(r => r.Name);
            eb.WithTitle($"Information About {user.Username}#{user.Discriminator}");
            if (String.IsNullOrEmpty(user.Nickname))
            {
                eb.AddField("ID", user.Id);
                eb.AddField("Roles", String.Join(", ", userRoles));
                eb.AddField("Joined Server", guildJoinDate);
                eb.AddField("Created Account", accountCreationDate);
                eb.WithThumbnailUrl(avatar);
                eb.WithColor(Lists.colorsArray[RandomizeColor]);
                await ReplyAsync("", false, eb.Build());
            }
            else
            {
                eb.AddField("Nickname", user.Nickname);
                eb.AddField("ID", user.Id);
                eb.AddField("Roles", String.Join(", ", userRoles));
                eb.AddField("Joined Server", guildJoinDate);
                eb.AddField("Created Account", accountCreationDate);
                eb.WithThumbnailUrl(avatar);
                eb.WithColor(Lists.colorsArray[RandomizeColor]);
                await ReplyAsync("", false, eb.Build());
            }
        }

        [Command("whois")]
        [Alias("who")]
        [Summary("View information for a user.")]
        public async Task WhoIsUserAsync()
        {
            var user = Context.Guild.GetUser(Context.User.Id);
            var avatar = user.GetAvatarUrl();
            var RandomizeColor = new Random().Next(Lists.colorsArray.Length);
            var eb = new EmbedBuilder();
            string guildJoinDate = String.Format("{0:d MMMM yyyy} at {0:hh}:{0:mm tt} GMT", user.JoinedAt);
            string accountCreationDate = String.Format("{0:d MMMM yyyy} at {0:hh}:{0:mm tt} GMT", user.CreatedAt);
            var userRoles = user.Roles.Select(r => r.Name);
            eb.WithTitle($"Information About {user.Username}#{user.Discriminator}");
            if (String.IsNullOrEmpty(user.Nickname))
            {
                eb.AddField("ID", user.Id);
                eb.AddField("Roles", String.Join(", ", userRoles));
                eb.AddField("Joined Server", guildJoinDate);
                eb.AddField("Created Account", accountCreationDate);
                eb.WithThumbnailUrl(avatar);
                eb.WithColor(Lists.colorsArray[RandomizeColor]);
                await ReplyAsync("", false, eb.Build());
            }
            else
            {
                eb.AddField("Nickname", user.Nickname);
                eb.AddField("ID", user.Id);
                eb.AddField("Roles", String.Join(", ", userRoles));
                eb.AddField("Joined Server", guildJoinDate);
                eb.AddField("Created Account", accountCreationDate);
                eb.WithThumbnailUrl(avatar);
                eb.WithColor(Lists.colorsArray[RandomizeColor]);
                await ReplyAsync("", false, eb.Build());
            }
        }

        [Command("herobrine")]
        [Summary("HE. COMES.")]
        public async Task SummonHerobrineAsync()
        {
            var nl = Environment.NewLine;
            await ReplyAsync($":skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-5:{nl}:skin-tone-5: :skin-tone-5: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-5: :skin-tone-5:{nl}:skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3:{nl}:skin-tone-3: :white_large_square: :white_large_square: :skin-tone-3: :skin-tone-3: :white_large_square: :white_large_square: :skin-tone-3:{nl}:skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3:{nl}:skin-tone-3: :skin-tone-3: :skin-tone-5: :skin-tone-3: :skin-tone-3: :skin-tone-5: :skin-tone-3: :skin-tone-3:{nl}:skin-tone-3: :skin-tone-3: :skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-5: :skin-tone-3: :skin-tone-3:{nl}:skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3: :skin-tone-3:");
        }

        [Command("invite")]
        public async Task Invite()
        {
            await Context.User.GetOrCreateDMChannelAsync();
            await Context.User.SendMessageAsync($"**Click this link to invite me to your server!** https://discordapp.com/api/oauth2/authorize?client_id=526748173327138825&scope=bot&permissions=8");
            await Context.Channel.SendMessageAsync($"**{Context.User.Username}, the invite link has been sent to you via Direct Messages. :mailbox:**");
        }
    }
}
