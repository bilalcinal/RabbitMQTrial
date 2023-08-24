using System.Text;
using RabbitMQ.Client;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://wjagymjh:CXETsmwe1_FRjhexW3DnApC73t3sxI4B@toad.rmq.cloudamqp.com/wjagymjh");

//Bağlantı etkinleştirme ve kanal oluşturma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//kuyruk oluşturma
channel.QueueDeclare(queue:"Example-queue", exclusive: false, durable:true);

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;
//Kuyruktan mesaj gönderme
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    channel.BasicPublish(exchange: "", routingKey: "Example-queue", body: message , basicProperties : properties);
}
Console.Read();

