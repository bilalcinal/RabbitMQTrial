using System.Text;
using RabbitMQ.Client;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://wjagymjh:CXETsmwe1_FRjhexW3DnApC73t3sxI4B@toad.rmq.cloudamqp.com/wjagymjh");

//Bağlantı etkinleştirme ve kanal oluşturma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange:"fanout-exchange-example",type: ExchangeType.Fanout);
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(exchange: "fanout-exchange-example", routingKey: string.Empty, body: message);
}


//*2//// Direct Exchange ////////
////Exchange Tanımlama
//channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

////Yollanan değer
//while (true)
//{
//    Console.Write("Mesaj : ");
//    string message = Console.ReadLine();
//    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

//    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage);
//}
//Console.Read();
////////////////////////



//*1//// Basic RabbitMQ ////////
////kuyruk oluşturma
//channel.QueueDeclare(queue:"Example-queue", exclusive: false, durable:true);

//IBasicProperties properties = channel.CreateBasicProperties();
//properties.Persistent = true;
////Kuyruktan mesaj gönderme
//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
//    channel.BasicPublish(exchange: "", routingKey: "Example-queue", body: message , basicProperties : properties);
//}
//Console.Read();
/////////////////////
