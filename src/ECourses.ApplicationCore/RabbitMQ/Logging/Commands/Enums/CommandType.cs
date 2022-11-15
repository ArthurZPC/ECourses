using System.Text.Json.Serialization;

namespace ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CommandType
    {
        Unknown = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
    }
}
