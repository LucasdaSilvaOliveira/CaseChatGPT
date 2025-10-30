using AutoMapper;
using CaseChatGPT.Web.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Web.Areas.Usuario.Controllers
{
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
