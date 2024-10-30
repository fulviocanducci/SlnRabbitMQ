using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShareLibrary;
using ShareLibrary.Extensions;
namespace CslAppReceiveMessageRabbitMQ
{
   internal class Program
   {
      static void Main(string[] args)
      {
         ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
         using (var connection = factory.CreateConnection())
         using (var channel = connection.CreateModel())
         {
            // Declara a fila
            channel.QueueDeclare(queue: "message", durable: false, exclusive: false, autoDelete: false, arguments: null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (object? model, BasicDeliverEventArgs ea) =>
            {
               Message message = ea.Body.ToMessage();
               Console.WriteLine($"[x] Recebido: {message.Id} {message.Text}");
            };

            // Começa a consumir mensagens
            channel.BasicConsume(queue: "message", autoAck: true, consumer: consumer);
            Console.WriteLine("[*] Aguardando mensagens. Para sair pressione CTRL+C");
            Console.ReadLine();
         }
      }
   }
}
