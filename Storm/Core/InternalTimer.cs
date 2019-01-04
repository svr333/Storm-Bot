using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Timers;
using Discord.WebSocket;
using Storm.Resources;

namespace Storm.Core
{
    internal static class InternalTimer
    {
        private static Timer internalTimer;

        internal static Task StartTimer()
        {

            internalTimer = new Timer()
            {
                Interval = 120000,
                AutoReset = true,
                Enabled = true
            };
            internalTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        internal static void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
        }
    }
}
