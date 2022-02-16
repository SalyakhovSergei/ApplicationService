using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Service.RabbitMQ
{
    public class Consumer: IConsumer
    {
        private const string _hostName = "localhost";
        private const string _queueName = "scoringAnswer";

        public string GetMessageFromQueue()
        {
            string message = "";

            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
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

                channel.BasicConsume(queue: _queueName,
                    autoAck: true,
                    consumer: consumer);
            }

            return message;
        }
    }
}