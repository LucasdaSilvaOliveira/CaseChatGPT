using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CaseChatGPT.Infra.Messaging
{
    public class RabbitMQPublisher
    {
        public async void Publish<T>(T message, string routingKey, string queue)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: routingKey, body: body);
            Console.WriteLine($" [x] Sent {message}");
            Console.WriteLine($"Message published with routing key '{routingKey}': {message}");
            Console.ReadLine();
        }
    }
}
