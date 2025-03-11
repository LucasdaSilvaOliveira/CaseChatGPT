using AutoMapper;
using CaseChatGPT.App.DTOs.Pedido;
using CaseChatGPT.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Api.Controllers
{
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoUseCases _pedidoUseCases;
        private readonly IMapper _mapper;
        public PedidoController(IPedidoUseCases pedidoUseCases, IMapper mapper)
        {
            _pedidoUseCases = pedidoUseCases;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedido = await _pedidoUseCases.GetPedidos();
            var pedidoDTO = _mapper.Map<IEnumerable<PedidoDTO>>(pedido);
            return Ok(pedidoDTO);
        }
    }
}
