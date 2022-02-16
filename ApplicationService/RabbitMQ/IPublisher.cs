using Application.Service.Models;

namespace Application.Service.RabbitMQ
{
    public interface IPublisher
    {
        void PublishToQueue(ApplicationQuery applicationQuery);
    }
}