using CaseChatGPT.Domain.Entities;

namespace CaseChatGPT.Domain.Interfaces.UseCases
{
    public interface IProdutoUseCases
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int id);
        void AddProduto(Produto produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(int id);
    }
}
