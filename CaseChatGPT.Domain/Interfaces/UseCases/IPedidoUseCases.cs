using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Interfaces.UseCases
{
    public interface IPedidoUseCases
    {
        Task<IEnumerable<Pedido>> GetPedidos();
        Task<Pedido> GetPedidoById(int id);
        void AddPedido(Pedido produto);
        void UpdatePedido(Pedido produto);
        void DeletePedido(int id);
    }
}
