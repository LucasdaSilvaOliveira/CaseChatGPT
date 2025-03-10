using AutoMapper;
using CaseChatGPT.App.DTOs.Produto;
using CaseChatGPT.Domain.Entities;

namespace CaseChatGPT.App.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, CriarProdutoDTO>().ReverseMap();
        }
    }
}
