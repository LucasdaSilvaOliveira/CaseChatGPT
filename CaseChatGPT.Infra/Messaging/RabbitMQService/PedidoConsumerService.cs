using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Messaging.RabbitMQService
{
    public class PedidoConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public PedidoConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var consumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();

            consumer.Consumer<string>("new-product");
        }
    }
}
