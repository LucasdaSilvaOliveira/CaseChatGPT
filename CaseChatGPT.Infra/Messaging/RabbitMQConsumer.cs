using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CaseChatGPT.Infra.Messaging
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        public async void Consumer<T>(string queue)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false,
               arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);

            Console.WriteLine($" [*] Waiting for messages in {queue}. To exit press CTRL+C");
            Console.ReadLine();
      
        }
    }
}
