namespace TeleReposter.Config
{
    public interface IConfig
    {
        void SetConfigPath(string configFilePath);
        void Update();
    }
}
