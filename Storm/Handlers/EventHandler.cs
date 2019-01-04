using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Storm.Core;
using System.Linq;
using Storm.Helpers;
using Storm.Modules;

namespace Storm.Handlers
{
    public class EventHandler
    {
        private readonly DiscordShardedClient _client;

        public EventHandler(DiscordShardedClient client)
        {
            _client = client;
        }

        public void InitDiscordEvents()
        {
            _client.ReactionAdded += ReactionAdded;
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var helpMessage = Global.HelpMessagesToTrack.FirstOrDefault();
            if (reaction.MessageId == helpMessage.MessageId)
            {
                if (reaction.Emote.Name == "🔨")
                {
                    var msg = reaction.Message;
                    await msg.Value.ModifyAsync(m => { m.Content = "Test."; });
                }

                if (reaction.Emote.Name == "💸")
                {

                }

                if (reaction.Emote.Name == "📂")
                {

                }

                if (reaction.Emote.Name == "📝")
                {

                }

                if (reaction.Emote.Name == "⛔")
                {

                }
            }
        }

        //var msg = message as SocketUserMessage;
        //
        //    if (msg == null) return;
        //if (msg.Channel == msg.Author.GetOrCreateDMChannelAsync()) return;
        //if (msg.Author.IsBot) return;
        //
        //var userAcc = UserAccounts.GetAccount(msg.Author);
        //DateTime now = DateTime.UtcNow;
        //    if (now<userAcc.LastMessage.AddSeconds(Lists.MessageRewardCooldown) || msg.Content.Length<Lists.MessageRewardMinLength)
        //{
        //    return;
        //}
        //var rnd = new Random();
        //userAcc.XP += (uint) rnd.Next(Lists.MessageRewardMinMax.Item1, Lists.MessageRewardMinMax.Item2 + 1);
        //userAcc.LastMessage = now;
        //
        //UserAccounts.SaveAccounts();
    }
}
