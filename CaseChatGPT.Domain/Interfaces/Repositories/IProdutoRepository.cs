﻿using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int id);
        Task<IEnumerable<Produto>> GetProdutosByUserId(string userId);
        void AddProduto(Produto produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(int id);
    }
}
