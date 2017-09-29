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
    }
}