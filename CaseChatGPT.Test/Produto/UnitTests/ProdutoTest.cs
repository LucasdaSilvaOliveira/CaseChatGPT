using AutoMapper;
using CaseChatGPT.Api.Controllers;
using CaseChatGPT.App.UseCases;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Domain.Interfaces.UseCases;
using CaseChatGPT.Domain.Entities;
using Moq;
using System.Collections.Generic;

namespace CaseChatGPT.Test.Produto.UnitTests
{
    public class ProdutoTest
    {

        [Fact]
        public async Task TestarUseCaseObterTodosProduto()
        {
            var repoMock = new Mock<IProdutoRepository>();
            var produtos = new Domain.Entities.Produto
            {
                Id = 1,
                Nome = "Produto Teste",
                Descricao = "Descricao Teste",
                Preco = 100.0m,
                UsuarioId = "1"
            };
            var produtoList = new List<Domain.Entities.Produto> { produtos };
            repoMock.Setup(rep => rep.GetProdutos()).ReturnsAsync(produtoList);

            var userCase = new ProdutoUseCases(repoMock.Object);

            var result = await userCase.GetProdutos(); 

            Assert.NotNull(result);
            //Assert.Single(result);
            Assert.Equal("Produto Teste", result.First().Nome);

            // Verifica se o método do repositório foi chamado exatamente uma vez
            repoMock.Verify(rep => rep.GetProdutos(), Times.Once);
        }
    }
}
