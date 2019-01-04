using System;

namespace Storm.Accounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }

        public uint Coins { get; set; }

        public uint XP { get; set; }

        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(XP / 500);
            }
        }

        public bool IsMuted { get; set; }

        public bool HasPass { get; set; }

        public DateTime LastMessage { get; set; } = DateTime.UtcNow;

        public DateTime LastDaily { get; set; } = DateTime.UtcNow.AddDays(-2);

        public uint NumberOfWarnings { get; set; }
    }
}
