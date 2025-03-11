using CaseChatGPT.App.DTOs.Usuario;
using CaseChatGPT.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioUseCases _usuarioUseCases;
        public UsuarioController(IUsuarioUseCases usuarioUseCases)
        {
            _usuarioUseCases = usuarioUseCases;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var usuarios = await _usuarioUseCases.GetUsuarios();

            var usuariosDTO = usuarios.Select(usuario => new UsuarioDTO
            {
                Nome = usuario.NomeCompleto,
                Email = usuario.Email!
            }).ToList();

            return Ok(usuariosDTO);
        }
    }
}
