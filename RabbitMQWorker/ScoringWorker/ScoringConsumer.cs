using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQWorker.Integration;

namespace RabbitMQWorker.ScoringWorker
{
    public class ScoringConsumer
    {
        private ScoringIntegration _integration;
        public ScoringConsumer()
        {
            _integration = new ScoringIntegration();
        }
        public string ConsumeFromQueue()
        {
            string message = "";

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "scoring",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);


                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var messageFromQueue = Encoding.UTF8.GetString(body);
                    message = messageFromQueue;
                };

                channel.BasicConsume(queue: "scoring",
                    autoAck: true,
                    consumer: consumer);

            }

            if (message != "")
            {
                _integration.Evaluate();
            }
            return message;
        }
    }
}