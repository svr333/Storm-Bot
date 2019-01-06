using Storm.Accounts;
using Storm.Resources;
using System;
using Discord.WebSocket;

namespace Storm.Helpers.Economy
{
    public class Daily
    {
        public static void GetDaily(SocketUser user)
        {
            var account = UserAccounts.GetAccount(user);
            var sinceLastDaily = DateTime.UtcNow - account.LastDaily;

            if (sinceLastDaily.TotalHours < 24)
            {
                var e = new InvalidOperationException(Lists.ExDailyTooSoon);
                e.Data.Add("sinceLastDaily", sinceLastDaily);
                throw e;
            }

            account.Coins += Lists.DailyCoinsGain;
            account.LastDaily = DateTime.UtcNow;

            UserAccounts.SaveAccounts();
        }
    }
}
