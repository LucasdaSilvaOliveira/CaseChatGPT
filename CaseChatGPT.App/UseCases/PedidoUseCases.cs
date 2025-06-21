using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Domain.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.App.UseCases
{
    public class PedidoUseCases : IPedidoUseCases
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoUseCases(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoById(id);
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> GetPedidos()
        {
            var pedidos = await _pedidoRepository.GetPedidos();
            return pedidos;
        }
        public void AddPedido(Pedido pedido)
        {
            // IMPLEMENTAR CHAMADA AO RABBITMQ AQUI
            _pedidoRepository.AddPedido(pedido);
        }

        public void UpdatePedido(Pedido pedido)
        {
           _pedidoRepository.UpdatePedido(pedido);
        }
        public void DeletePedido(int id)
        {
            _pedidoRepository.DeletePedido(id);
        }
    }
}
