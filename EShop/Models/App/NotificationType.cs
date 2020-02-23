using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EShop.Models.App
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationType
    {
        Success,
        Error,
        Info,
    }
}
