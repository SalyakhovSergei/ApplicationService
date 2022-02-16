using System;
using System.Threading;
using RabbitMQWorker.ScoringWorker;

namespace RabbitMQWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(1000);
                ScoringConsumer consumer = new ScoringConsumer();
                var answer = consumer.ConsumeFromQueue();
                if (answer != "")
                {
                    Console.WriteLine(answer);
                }
                
            }
            
        }
    }
}
