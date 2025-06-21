using CaseChatGPT.App.Interfaces.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CaseChatGPT.Infra.Messaging.RabbitMQService
{
    public class ProdutoConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ProdutoConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<IRabbitMQConsumer>();
                await consumer.Consumer<string>("new-product");
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao consumir a fila: {ex.Message}");
            }
        }

    }
}
