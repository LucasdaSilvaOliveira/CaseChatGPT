using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces;
using CaseChatGPT.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void DeleteProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }

        public async Task<Produto> GetProdutoById(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null) throw new NullReferenceException("Produto não encontrado");
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return produtos;
        }

        public void UpdateProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }
    }
}
