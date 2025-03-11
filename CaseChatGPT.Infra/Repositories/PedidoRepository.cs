using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseChatGPT.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly BancoContext _context;
        public PedidoRepository(BancoContext context)
        {
            _context = context;
        }

        public void AddPedido(Pedido pedido)
        {
            if(pedido == null ||
                pedido.ProdutoId == 0 ||
                pedido.UsuarioId == null) throw new Exception("Pedido não pode ser nulo");

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            if(id == 0) throw new Exception("Problema no id do produto");

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) throw new Exception("Pedido não encontrado");
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> GetPedidos()
        {
            var pedidos = await _context.Pedidos.ToListAsync();
            return pedidos;
        }

        public void UpdatePedido(Pedido pedido)
        {
            if (pedido == null ||
              pedido.ProdutoId == 0 ||
              pedido.UsuarioId == null) throw new Exception("Pedido não pode ser nulo");

            _context.Pedidos.Update(pedido);
            _context.SaveChanges();
        }

        public void DeletePedido(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            if(pedido == null) throw new Exception("Pedido não encontrado");
            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();
        }
    }
}
