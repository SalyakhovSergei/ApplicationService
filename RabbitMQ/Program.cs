using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
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
                    Console.WriteLine(messageFromQueue);
                    message = messageFromQueue;
                };

                channel.BasicConsume(queue: "scoring",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(message);

            }
        }
    }
}
