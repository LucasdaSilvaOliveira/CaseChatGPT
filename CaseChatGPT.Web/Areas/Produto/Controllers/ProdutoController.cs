﻿using AutoMapper;
using CaseChatGPT.Web.DTOs.Produto;
using CaseChatGPT.Web.Services.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CaseChatGPT.Web.Areas.Produto.Models;

namespace CaseChatGPT.Web.Areas.Controllers
{
    [Authorize]
    [Area("Produto")]
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
            try
            {
                var userId = _userManager.GetUserId(User)!;

                var produtos = await _produtoService.ObterProdutosPorUserId(userId);

                var model = _mapper.Map<List<ProdutoViewModel>>(produtos);

                return View(model);

            } catch
            {
                return RedirectToAction("Index", "Login", new { area = "Login" });
            }
         

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
                var userId = _userManager.GetUserId(User)!;
                var produtoDB = await _produtoService.ObterProdutoPorId(model.Id);
                var produto = _mapper.Map<AtualizarProdutoDTO>(produtoDB);
                produto.UsuarioId = userId;
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
        public async Task<IActionResult> Adicionar(AdicionarProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User)!;
                var produto = _mapper.Map<AdicionarProdutoDTO>(model);
                produto.UsuarioId = userId;

                await _produtoService.AdicionarProduto(produto);

                return RedirectToAction("Index", "Produto", new { area = "Produto" });

            }
            return View(model);
        }
    }
}
