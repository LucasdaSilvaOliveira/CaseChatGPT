using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int id);
        Task<Produto> AddProduto(Produto produto);
        Task<Produto> UpdateProduto(Produto produto);
        Task<Produto> DeleteProduto(int id);
    }
}
