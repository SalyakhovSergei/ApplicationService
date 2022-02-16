namespace Application.Service.RabbitMQ
{
    public interface IConsumer
    {
        string GetMessageFromQueue();
    }
}