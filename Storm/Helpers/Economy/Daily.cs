using Storm.Accounts;
using Storm.Resources;
using System;

namespace Storm.Helpers.Economy
{
    public class Daily : IDailyCoins
    {
        private readonly IUserAccountProvider userAccountProvider;

        public Daily(IUserAccountProvider userAccountProvider)
        {
            this.userAccountProvider = userAccountProvider;
        }

        public void GetDaily(ulong userId)
        {
            var account = userAccountProvider.GetById(userId);
            var sinceLastDaily = DateTime.UtcNow - account.LastDaily;

            if (sinceLastDaily.TotalHours < 24)
            {
                var e = new InvalidOperationException(Lists.ExDailyTooSoon);
                e.Data.Add("sinceLastDaily", sinceLastDaily);
                throw e;
            }

            account.Coins += Lists.DailyCoinsGain;
            account.LastDaily = DateTime.UtcNow;

            userAccountProvider.SaveByIds(userId);
        }
    }
}
