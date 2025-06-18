using AutoMapper;
using CaseChatGPT.App.DTOs.Produto;
using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Web.DTOs.Produto;
using CaseChatGPT.Web.Models;
using CaseChatGPT.Web.Services.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaseChatGPT.Web.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public ProdutoController(IProdutoService produtoService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _produtoService = produtoService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User)!;

            var produtos = await _produtoService.ObterProdutosPorUserId(userId);

            var model = _mapper.Map<List<ProdutoViewModel>>(produtos);

            return View(model);

        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _produtoService.ObterProdutoPorId(id);

            var model = _mapper.Map<ProdutoViewModel>(produto);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _produtoService.ObterProdutoPorId(id);
            var model = _mapper.Map<ProdutoViewModel>(produto);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var produtoDB = await _produtoService.ObterProdutoPorId(model.Id);
                var produto = _mapper.Map<AtualizarProdutoDTO>(produtoDB);

                await _produtoService.AtualizarProduto(produto);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User)!;

                var produto = _mapper.Map<AdicionarProdutoDTO>(model);
                produto.UserId = userId;

                await _produtoService.AdicionarProduto(produto);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
