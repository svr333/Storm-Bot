using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Storm.Accounts;
using Storm.Resources;

namespace Storm.Handlers
{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
    public class MessageRewardHandler
    {
        public async Task UserSentMessage(SocketMessage message)
        {
            var user = message.Author as SocketGuildUser;
            if (user != null && user.IsBot) return;
            var channel = message.Channel as SocketTextChannel;
            var msg = message.Content;
            int msgLength = msg.Length;
            var userAccount = UserAccounts.GetAccount(user);

            if (channel != null)
            {
                if (msgLength > Lists.MessageRewardMinLength)
                {
                    userAccount.XP += Lists.XpPerMessageGain;
                }

                UserAccounts.SaveAccounts();
            }
        }

        public async Task OnUserMessageSent(SocketMessage message)
        {
            UserSentMessage(message);
        }
    }
}
