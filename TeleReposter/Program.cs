using System;
using System.Threading;
using TeleReposter.Extensions.String;

namespace TeleReposter
{
    class Program
    {
        static void Main(string[] args)
        {
            const string configFilePath = @"<config_file_path>";

            var config = System.IO.File.ReadAllText(configFilePath).FromJsonTo<MediaResourcesConfig>();

            var timerPeriod = config.settings.timerPeriod;

            var autoReposter = new AutoReposter(configFilePath);

            var repostMedia = new TimerCallback(autoReposter.Repost);
            var period = new TimeSpan(timerPeriod.hours, timerPeriod.minutes, timerPeriod.seconds);

            var timer = new Timer(repostMedia, null, new TimeSpan(), period);

            Console.ReadKey();
        }
    }
}
