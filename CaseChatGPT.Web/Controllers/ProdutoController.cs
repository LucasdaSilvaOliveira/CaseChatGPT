using AutoMapper;
using CaseChatGPT.Web.Models;
using CaseChatGPT.Web.Services.Produto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Web.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.ObterProdutos();

            var model = _mapper.Map<List<ProdutoViewModel>>(produtos);

            return View(model);

        }
    }
}
