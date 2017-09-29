using TeleReposter.Config;
using TeleReposter.Extensions.String;

namespace TeleReposter.Services
{
    public class ConfigService
    {
        public T Get<T>(string configFilePath) where T : IConfig
        {
            var config = System.IO.File.ReadAllText(configFilePath).FromJsonTo<T>();
            config.SetConfigPath(configFilePath);

            return config;
        }
    }
}