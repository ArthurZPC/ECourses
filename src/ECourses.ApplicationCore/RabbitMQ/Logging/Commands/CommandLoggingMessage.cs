using ECourses.ApplicationCore.RabbitMQ.Logging.Commands.Enums;
using MediatR;
using System.Text.Json;

namespace ECourses.ApplicationCore.RabbitMQ.Logging.Commands
{
    public class CommandLoggingMessage<T> where T : IRequest
    {
        public string CommandName => typeof(T).Name;
        public CommandType CommandType { get; set; }

        public T CommandContent { get; set; } = default!;

        public DateTime? ExecutedAt { get; set; }


        public CommandLoggingMessage(T command, CommandType commandType, DateTime? executedAt)
        {
            CommandContent = command;
            CommandType = commandType;
            ExecutedAt = executedAt;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
