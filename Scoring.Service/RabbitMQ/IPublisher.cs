using Scoring.Service.Models;

namespace Scoring.Service.RabbitMQ
{
    public interface IPublisher
    {
        void PublishToQueue(Response response);
    }
}