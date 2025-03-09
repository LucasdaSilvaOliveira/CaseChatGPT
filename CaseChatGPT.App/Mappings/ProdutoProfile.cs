using AutoMapper;
using CaseChatGPT.App.DTOs.Produto;
using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.App.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ObterProdutosDTO>().ReverseMap();
        }
    }
}
