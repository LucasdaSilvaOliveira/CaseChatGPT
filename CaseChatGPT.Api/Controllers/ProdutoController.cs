using AutoMapper;
using CaseChatGPT.App.DTOs.Produto;
using CaseChatGPT.App.UseCases;
using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoUseCases _produtoUseCase;
        private readonly IMapper _mapper;
        public ProdutoController(IProdutoUseCases produtoUseCase, IMapper mapper)
        {
            _produtoUseCase = produtoUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoUseCase.GetProdutos();
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto(int id)
        {
            try
            {
                var produto = await _produtoUseCase.GetProdutoById(id);
                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddProduto([FromBody] CriarProdutoDTO produtoDTO)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoDTO);
                _produtoUseCase.AddProduto(produto);
                return Ok("Produto criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoDTO newProduto)
        {
            try
            {
                var produto = _mapper.Map<Produto>(newProduto);
                produto.Id = id;

                _produtoUseCase.UpdateProduto(produto);
                return Ok("Produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            try
            {
                _produtoUseCase.DeleteProduto(id);
                return Ok("Produto deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
