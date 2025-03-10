﻿using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Interfaces.UseCases
{
    public interface IProdutoUseCases
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int id);
        void AddProduto(Produto produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(Produto produto);
    }
}
