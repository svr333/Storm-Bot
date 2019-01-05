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
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private readonly DiscordSocketClient _client;
        private readonly CommandHandler _handler;

        public EventHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public void InitDiscordEvents()
        {
            _client.MessageReceived += MessageReceived;
            _client.ReactionAdded += ReactionAdded;
            _client.JoinedGuild += JoinedGuild;
        }

        private static async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {

        }

        private async Task MessageReceived(SocketMessage message)
        {

        }

        private async Task JoinedGuild(SocketGuild guild)
        {

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
