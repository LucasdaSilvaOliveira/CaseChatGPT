using AutoMapper;
using CaseChatGPT.App.DTOs.Pedido;
using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.App.Mappings
{
    public class PedidoProfile :Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<Pedido, CriarPedidoDTO>().ReverseMap();
        }
    }
}
