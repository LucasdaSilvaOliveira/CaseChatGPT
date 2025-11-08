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

            foreach (var item in model)
            {
                var userRole = await _usuarioService.ObterRoleUsuarioPorId(item.Id);
                if (userRole != null)
                {
                    item.RoleName = userRole.Name;
                    item.RoleDescription = userRole.Description;

                    switch (userRole.Name)
                    {
                        case "Admin":
                            item.BadgeType = "bg-primary";
                            break;
                        case "User":
                            item.BadgeType = "bg-secondary";
                            break;  
                        case "Guest":
                            item.BadgeType = "bg-warning";
                            break;  
                        case "Manager":
                            item.BadgeType = "bg-success";
                            break;
                        default:
                            item.BadgeType = "bg-secondary";
                            break;
                    }
                }
            }

            return View(model);
        }
    }
}
