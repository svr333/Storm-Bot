using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Storm.Handlers
{
    public class EventHandler
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private readonly DiscordSocketClient _client;
        private readonly CommandHandler _handler;
        private readonly MessageRewardHandler _msgRewardHandler;

        public EventHandler(DiscordSocketClient client, MessageRewardHandler msgRewardHandler)
        {
            _client = client;
            _msgRewardHandler = msgRewardHandler;
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
            if (message.Author.IsBot)
            {
                return;
            }
            _msgRewardHandler.OnUserMessageSent(message);
        }

        private async Task JoinedGuild(SocketGuild guild)
        {

        }
    }
}
