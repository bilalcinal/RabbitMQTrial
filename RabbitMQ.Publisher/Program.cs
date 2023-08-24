using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new("amqps://wjagymjh:CXETsmwe1_FRjhexW3DnApC73t3sxI4B@toad.rmq.cloudamqp.com/wjagymjh");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue:"Example-queue", exclusive: false);

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: "", routingKey:"Example-queue", body: message);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    channel.BasicPublish(exchange: "", routingKey: "Example-queue", body: message);
}
Console.Read();

