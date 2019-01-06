using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Storm.Accounts;
using Storm.Resources;

namespace Storm.Helpers.Economy
{
    public class Transfer
    {
        public static void TransferCoins(SocketUser user, SocketUser target, uint amount)
        {
            var userAccount = UserAccounts.GetAccount(user);
            var targetAccount = UserAccounts.GetAccount(target);

            if (amount > userAccount.Coins)
            {
                var e = new InvalidOperationException(Lists.ExTransferNotEnoughFunds);
                throw e;
            }

            if (target.IsBot)
            {
                var e = new InvalidOperationException(Lists.ExTransferToBot);
                throw e;
            }

            if (user == target)
            {
                var e = new InvalidOperationException(Lists.ExTransferSameUser);
                throw e;
            }

            userAccount.Coins -= amount;
            targetAccount.Coins += amount;

            UserAccounts.SaveAccounts();
        }
    }
}
