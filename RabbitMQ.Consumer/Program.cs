using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new();
factory.Uri = new("amqps://wjagymjh:CXETsmwe1_FRjhexW3DnApC73t3sxI4B@toad.rmq.cloudamqp.com/wjagymjh");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "Example-queue", exclusive: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "Example-queue", false, consumer);
consumer.Received += (sender,e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();