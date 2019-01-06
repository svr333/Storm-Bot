using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using Storm.Core;

namespace Storm.Helpers
{
    public class AwaitUserInput
    {
        public static async Task<SocketMessage> AwaitInput(ulong userId, ulong channelId, int delay)
        {
            SocketMessage response = null;
            var cancel = new CancellationTokenSource();
            var waiter = Task.Delay(delay, cancel.Token);

            Global.Client.MessageReceived += OnMessageReceived;
            try
            {
                await waiter;
            }
            catch (TaskCanceledException)
            {
            }

            Global.Client.MessageReceived -= OnMessageReceived;

            return response;

            async Task OnMessageReceived(SocketMessage message)
            {
                if (message.Author.Id != userId || message.Channel.Id != channelId)
                    return;
                response = message;
                cancel.Cancel();
                await Task.CompletedTask;
            }
        }
    }
}
