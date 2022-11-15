namespace ECourses.ApplicationCore.RabbitMQ.Interfaces
{
    public interface IRabbitMQService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}
