using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Scoring.Service.Models;

namespace Scoring.Service.RabbitMQ
{
    public class Publisher: IPublisher
    {
        private const string _hostName = "localhost";
        private const string _queueName = "scoringAnswer";

        public void PublishToQueue(Response response)
        {
            var body = JsonConvert.SerializeObject(response);

            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = Encoding.UTF8.GetBytes(body);

                channel.BasicPublish(exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: message);

                Console.WriteLine("Message sent to Rabbit");
            }
        }
    }
}