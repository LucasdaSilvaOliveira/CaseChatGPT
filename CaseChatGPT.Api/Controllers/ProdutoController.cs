using CaseChatGPT.App.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoUseCases _produtoUseCase;
        public ProdutoController(ProdutoUseCases produtoUseCase)
        {
            _produtoUseCase = produtoUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoUseCase.GetProdutos();
            return Ok(produtos);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProduto(int id)
        //{
        //    var produto = await _produtoRepository.GetProdutoById(id);
        //    return Ok(produto);
        //}
    }
}
