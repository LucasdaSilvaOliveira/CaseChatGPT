using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseChatGPT.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BancoContext _context;
        public ProdutoRepository(BancoContext context)
        {
            _context = context;
        }

        public void AddProduto(Produto produto)
        {
            if (produto == null ||
                produto.Nome == null ||
                produto.Descricao == null ||
                produto.Preco == 0) throw new Exception("Produto não pode ser nulo");

            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public async Task<Produto> GetProdutoById(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null) throw new Exception("Produto não encontrado");
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetProdutosByUserId(string userId)
        {
            var produto = await _context.Produtos.Where(p => p.UsuarioId == userId).ToListAsync();

            if (produto == null) throw new Exception("Produto não encontrado");
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return produtos;
        }

        public void UpdateProduto(Produto produto)
        {
            if (produto == null ||
                 produto.Nome == null ||
                 produto.Descricao == null ||
                 produto.Preco == 0) throw new Exception("Produto não pode ser nulo");

            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void DeleteProduto(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto == null) throw new Exception("Produto não encontrado");
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
    }
}
