using AutoMapper;
using CaseChatGPT.Web.Areas.Usuario.Models;
using CaseChatGPT.Web.Services.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Web.Areas.Usuario.Controllers
{
    [Authorize]
    [Area("Usuario")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(IMapper mapper, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
        }
        public async Task<IActionResult> Index()
        {
            var usuariosDTO = await _usuarioService.ObterUsuarios();

            var model = _mapper.Map<List<UsuarioViewModel>>(usuariosDTO);

            return View(model);
        }
    }
}
