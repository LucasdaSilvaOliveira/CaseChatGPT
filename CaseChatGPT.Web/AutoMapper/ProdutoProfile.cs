﻿using AutoMapper;
using CaseChatGPT.Web.DTOs.Produto;
using CaseChatGPT.Web.Areas.Produto.Models;

namespace CaseChatGPT.Web.AutoMapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ObterProdutoDTO, ProdutoViewModel>().ReverseMap();
            CreateMap<ObterProdutoDTO, AtualizarProdutoDTO>().ReverseMap();
            CreateMap<ProdutoViewModel, AtualizarProdutoDTO>().ReverseMap();
            CreateMap<ProdutoViewModel, AdicionarProdutoDTO>().ReverseMap();
            CreateMap<AdicionarProdutoViewModel, AdicionarProdutoDTO>().ReverseMap();
        }
    }
}
