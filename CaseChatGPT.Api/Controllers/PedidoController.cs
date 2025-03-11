using AutoMapper;
using CaseChatGPT.App.DTOs.Pedido;
using CaseChatGPT.App.DTOs.Produto;
using CaseChatGPT.App.UseCases;
using CaseChatGPT.Domain.Entities;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            try
            {
                var pedido = await _pedidoUseCases.GetPedidoById(id);
                var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddPedido([FromBody] CriarPedidoDTO pedidoDTO)
        {
            try
            {
                var pedido = _mapper.Map<Pedido>(pedidoDTO);
                _pedidoUseCases.AddPedido(pedido);
                return Ok("Pedido criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, [FromBody] PedidoDTO newPedido)
        {
            try
            {
                var pedido = _mapper.Map<Pedido>(newPedido);
                pedido.Id = id;

                _pedidoUseCases.UpdatePedido(pedido);
                return Ok("Pedido atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            try
            {
                _pedidoUseCases.DeletePedido(id);
                return Ok("Pedido deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
