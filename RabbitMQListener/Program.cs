using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQListener
{
    class Program
    {
        static void Main(string[] args)
        {
            ScoringIntegration scoringIntegration = new ScoringIntegration();

            string message = "";

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();


            channel.QueueDeclare(queue: "scoring",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: "scoring",
                autoAck: true,
                consumer: consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var messageFromQueue = Encoding.UTF8.GetString(body);
                Console.WriteLine(messageFromQueue);
                message = messageFromQueue;
                scoringIntegration.Evaluate();
            };

            Console.ReadLine();
        }
    }
}
