using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TeleReposter.Config;

namespace TeleReposter
{
    public class Resource
    {
        public string name { get; set; }
        public string chatId { get; set; }
        public string logChatId { get; set; }
    }

    public class LastReposted
    {
        public string resource { get; set; }
        public string itemId { get; set; }
        public DateTime dateTime{ get; set; }
    }

    public class MediaResourcesConfig : IConfig
    {
        public List<Resource> resources { get; set; }
        public List<LastReposted> lastReposted { get; set; }
        public Settings settings { get; set; }

        private string _configFilePath;

        public void SetConfigPath(string configFilePath)
        {
            this._configFilePath = configFilePath;
        }

        public void Update()
        {
            System.IO.File.WriteAllText(this._configFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }

    public class Settings
    {
        public long chatId { get; set; }
        public TimerPeriod timerPeriod { get; set; }
        public int contentCount { get; set; }
    }

    public class TimerPeriod
    {
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
    }
}