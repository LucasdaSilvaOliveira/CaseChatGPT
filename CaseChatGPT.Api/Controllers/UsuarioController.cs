using CaseChatGPT.App.DTOs.Usuario;
using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioUseCases _usuarioUseCases;
        public UsuarioController(IUsuarioUseCases usuarioUseCases)
        {
            _usuarioUseCases = usuarioUseCases;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioUseCases.GetUsuarios();

            var usuariosDTO = usuarios.Select(usuario => new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.NomeCompleto,
                Email = usuario.Email!,
                UserName = usuario.UserName!
            }).ToList();

            return Ok(usuariosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(string id)
        {
            try
            {
                var usuario = await _usuarioUseCases.GetUsuarioById(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                var usuarioDTO = new UsuarioDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.NomeCompleto,
                    Email = usuario.Email!
                };

                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]    
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CriarUsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = new Usuario
                {
                    NomeCompleto = usuarioDTO.NomeCompleto,
                    Email = usuarioDTO.Email,
                    UserName = usuarioDTO.UserName,
                    NormalizedEmail = usuarioDTO.Email.ToUpper(),
                    NormalizedUserName = usuarioDTO.UserName.ToUpper()
                };

                await _usuarioUseCases.AddUsuario(usuario, usuarioDTO.Senha);

                return Ok("Usuário criado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(string id, [FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = id,
                    NomeCompleto = usuarioDTO.Nome,
                    Email = usuarioDTO.Email
                };

                await _usuarioUseCases.UpdateUsuario(usuario);

                return Ok("Usuário atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            try
            {
                var usuario = await _usuarioUseCases.GetUsuarioById(id);
                await _usuarioUseCases.DeleteUsuario(usuario);

                return Ok("Usuário deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
