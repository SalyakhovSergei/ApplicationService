namespace Scoring.Service.RabbitMQ
{
    public interface IConsumer
    {
        string GetMessageFromQueue();
    }
}