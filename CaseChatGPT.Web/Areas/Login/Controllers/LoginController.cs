using CaseChatGPT.Web.Areas.Login.Models;
using CaseChatGPT.Web.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CaseChatGPT.Web.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var loginRealizado = await _authService.Login(model.UserName, model.Password);

            if(loginRealizado) return RedirectToAction("Index", "Home", new { area = "" });

            return View();
        }
    }
}
