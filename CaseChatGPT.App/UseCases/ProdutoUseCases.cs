using CaseChatGPT.App.Interfaces.RabbitMQ;
using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Domain.Interfaces.UseCases;

namespace CaseChatGPT.App.UseCases
{
    public class ProdutoUseCases : IProdutoUseCases
    {
        private readonly IProdutoRepository _produtoRepository;
        //private readonly IRabbitMQPublisher _rabbitMQPublisher;
        public ProdutoUseCases(IProdutoRepository produtoRepository/*, IRabbitMQPublisher rabbitMQPublisher*/)
        {
            _produtoRepository = produtoRepository;
            //_rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetProdutos();
            return produtos;
        }
        public async Task<Produto> GetProdutoById(int id)
        {
            var produto = await _produtoRepository.GetProdutoById(id);
            return produto;
        }

        public Task<IEnumerable<Produto>> GetProdutosByUserId(string userId)
        {
            var produto = _produtoRepository.GetProdutosByUserId(userId);
            return produto;
        }

        public void AddProduto(Produto produto)
        {
            // IMPLEMENTAR CHAMADA AO RABBITMQ AQUI
            //_rabbitMQPublisher.Publish(produto, "product-key", "new-product");
            _produtoRepository.AddProduto(produto);
        }

        public void UpdateProduto(Produto produto)
        {
            _produtoRepository.UpdateProduto(produto);
        }

        public void DeleteProduto(int id)
        {
            _produtoRepository.DeleteProduto(id);
        }
    }
}
