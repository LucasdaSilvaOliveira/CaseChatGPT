using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Context
{
    public class BancoContextFactory : IDesignTimeDbContextFactory<BancoContext>
    {
        public BancoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-H156HPH\\SQLEXPRESS;Database=GestaoPedidos;Trusted_Connection=True;TrustServerCertificate=True;");

            return new BancoContext(optionsBuilder.Options);
        }
    }
}
