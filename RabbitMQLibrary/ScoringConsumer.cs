using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQLibrary
{
    public class ScoringConsumer : IScoringConsumer
    {
        public void CallScoring()
        {
            ScoringIntegration scoringIntegration = new ScoringIntegration();

            scoringIntegration.Evaluate();

        }
    }
}