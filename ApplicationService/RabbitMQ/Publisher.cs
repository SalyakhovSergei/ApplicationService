using System;
using System.Text;
using Application.Service.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Application.Service.RabbitMQ
{
    public class Publisher: IPublisher
    {
        public void PublishToQueue(ApplicationQuery applicationQuery)
        {
            var body = JsonConvert.SerializeObject(applicationQuery);

            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "scoring",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = Encoding.UTF8.GetBytes(body);

                channel.BasicPublish(exchange:"",
                    routingKey:"scoring",
                    basicProperties:null,
                    body: message);

                Console.WriteLine("Message sent to Rabbit");
            }

        }
    }
}