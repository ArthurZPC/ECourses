using ECourses.ApplicationCore.RabbitMQ.Configuration;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ECourses.ApplicationCore.RabbitMQ.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQOptions _rabbitMQOptions;

        public RabbitMQService(IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;
        }
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);

            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _rabbitMQOptions.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQOptions.QueueName,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: _rabbitMQOptions.QueueName,
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
