using CaseChatGPT.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Context
{
    public class BancoContext : IdentityDbContext<Usuario, Role, string>
    //public class BancoContext : IdentityDbContext<IdentityUser>
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>().HasDiscriminator<string>("Discriminator")
                .HasValue<Usuario>("Usuario"); // Define o valor que será salvo na coluna
        }
    }

}
