using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Infra.Context;
using CaseChatGPT.Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Adiciona o DbContext ao DI
            services.AddDbContext<BancoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

         //   services.AddIdentityCore<Usuario>()
         //.AddEntityFrameworkStores<BancoContext>();

            // Registra o repositório como Scoped
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
