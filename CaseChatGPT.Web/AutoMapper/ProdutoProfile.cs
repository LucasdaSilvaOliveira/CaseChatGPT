using AutoMapper;
using CaseChatGPT.Web.DTOs.Produto;
using CaseChatGPT.Web.Models;

namespace CaseChatGPT.Web.AutoMapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ObterProdutoDTO, ProdutoViewModel>().ReverseMap();
        }
    }
}
