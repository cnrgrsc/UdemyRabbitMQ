using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();  //connectinfactory bağlantı için instanse aldık
            factory.Uri = new Uri("amqps://oxoahzie:4Yvg3i2Q7AX8yey86Og09c5szzAyrjie@toad.rmq.cloudamqp.com/oxoahzie"); //url kopyalamak için

            using var connection = factory.CreateConnection(); // yeni bir bağlantı oluşturduk 

            var channel = connection.CreateModel(); // kanalı oluşturduk

            channel.QueueDeclare("hello-queue", true, false, false); //kuyruğa atma sırasını oluşturduk 

            Enumerable.Range(1, 100).ToList().ForEach(x => //giden mesajı çoklu gönderdiğimiz için foreach olarak döndük
            {

                string message = $"Message {x}"; 

                var messageBody = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

                Console.WriteLine($"Mesaj Gönderilmiştir:{message}");
            });


            Console.ReadLine();
        }
    }
}
