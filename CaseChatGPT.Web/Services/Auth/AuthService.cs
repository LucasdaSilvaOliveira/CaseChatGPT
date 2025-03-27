using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace CaseChatGPT.Web.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<bool> Login(string username, string password)
        //{
        //    string httpClientName = _configuration["HttpClientName"]!;

        //    using HttpClient client = _httpClientFactory.CreateClient(httpClientName);

        //    var response = await client.PostAsJsonAsync("Auth/Login", new { username, password });

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
        //        _httpContextAccessor.HttpContext.Session.SetString("AuthToken", content!.Token);
        //        return true;
        //    };

        //    return false;
        //}

        public async Task<bool> Login(string username, string password)
        {
            string httpClientName = _configuration["HttpClientName"]!;
            using HttpClient client = _httpClientFactory.CreateClient(httpClientName);

            var response = await client.PostAsJsonAsync("Auth/Login", new { username, password });

            if (!response.IsSuccessStatusCode)
            {
                return false; // Falha na autenticação
            }

            var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
            string token = content!.Token;

            // Armazena o token na sessão
            _httpContextAccessor.HttpContext.Session.SetString("AuthToken", token);

            // Criar claims do usuário
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("AuthToken", token) // Guarda o token como claim
            };

            var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Autenticar o usuário na camada Web com Identity Cookies
            await _httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

            return true;
        }


        // ESSE MÉTODO VAI TER QUE SAIR DAQUI
        public async Task<bool> ObterProdutos()
        {
            string httpClientName = _configuration["HttpClientName"]!;

            using HttpClient client = _httpClientFactory.CreateClient(httpClientName);

            // Recupera o token da sessão
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                return false; // Usuário não autenticado
            }

            var response = await client.GetAsync("Produto");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return true;
            };

            return false;
        }

        public class TokenResponse
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}
