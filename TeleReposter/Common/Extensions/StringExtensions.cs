using Newtonsoft.Json;

namespace TeleReposter.Extensions.String
{
    public static class StringExtensions
    {
        public static T FromJsonTo<T>(this string value)
        {
            return value.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static T TryGetJson<T>(this string value)
        {
            try
            {
                return value.FromJsonTo<T>();
            }
            catch (Exception ex)
            {
                this.Bot.LogToChannel(-1001107875397, ex);
            }

            return defaultnull;
        }
    }
}