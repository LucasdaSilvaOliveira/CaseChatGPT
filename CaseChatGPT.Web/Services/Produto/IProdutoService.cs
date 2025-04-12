using CaseChatGPT.Web.DTOs.Produto;

namespace CaseChatGPT.Web.Services.Produto
{
    public interface IProdutoService
    {
        Task<List<ObterProdutoDTO>> ObterProdutos();
        Task<ObterProdutoDTO> ObterProdutoPorId(int id);
        Task<List<ObterProdutoDTO>> ObterProdutosPorUserId(string userId);
        Task<bool> AdicionarProduto(AdicionarProdutoDTO produto);
        Task<bool> AtualizarProduto(AtualizarProdutoDTO produto);
        Task<bool> RemoverProduto(int id);
    }
}
