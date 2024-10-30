using RabbitMQ.Client;
using ShareLibrary;
using ShareLibrary.Extensions;

namespace CslAppSendMessageRabbitMQ
{
   internal class Program
   {
      static void Main(string[] args)
      {
         ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
         using (IConnection? connection = factory.CreateConnection())
         using (IModel channel = connection.CreateModel())
         {
            channel.QueueDeclare(queue: "message", durable: false, exclusive: false, autoDelete: false, arguments: null);
            while (true)
            {
               Console.Clear();
               Console.Write("Digite o texto: ");
               string text = Console.ReadLine();
               if (!string.IsNullOrEmpty(text))
               {
                  Message message = new(text);

                  // Publica a mensagem na fila
                  channel.BasicPublish(exchange: "", routingKey: "message", basicProperties: null, body: message.ToByteArray());
                  Console.WriteLine($"[x] Enviado: {message.Text}");
                  Console.WriteLine("Pressione <enter>");
                  Console.ReadKey();
               }
               else
               {
                  Console.Write("Digite o texto, pressione <enter>");
                  Console.ReadKey();
               }
            }
         }
      }
   }
}
