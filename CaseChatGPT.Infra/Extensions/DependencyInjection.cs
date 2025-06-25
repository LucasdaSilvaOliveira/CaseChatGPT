using CaseChatGPT.App.Interfaces.RabbitMQ;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Infra.Context;
using CaseChatGPT.Infra.Messaging;
using CaseChatGPT.Infra.Messaging.RabbitMQService;
using CaseChatGPT.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CaseChatGPT.Infra.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BancoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();
            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();

            //services.AddHostedService<ProdutoConsumerService>();

            return services;
        }
    }
}
