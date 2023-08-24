using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://wjagymjh:CXETsmwe1_FRjhexW3DnApC73t3sxI4B@toad.rmq.cloudamqp.com/wjagymjh");

//Bağlantı etkinleştirme ve kanal oluşturma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchange tanımlama
channel.ExchangeDeclare(exchange: "direct-exchange-example", type:ExchangeType.Direct);

//Yolanan değerleri tüketir
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer:consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
Console.Read();

////// Basic RabbitMQ //////
////kuyruk oluşturma
//channel.QueueDeclare(queue: "Example-queue", exclusive: false, durable: true);

////Kuyruktan mesaj okuma
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: "Example-queue", autoAck: false, consumer);
//channel.BasicQos(0, 1, false);
//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
//};

//Console.Read();
//////////////////////////