using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<bool> Login(string username, string password)
        {
            string httpClientName = _configuration["HttpClientName"]!;
            
            using HttpClient client = _httpClientFactory.CreateClient(httpClientName);

            var response = await client.PostAsJsonAsync("Auth/Login", new { username, password });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
                _httpContextAccessor.HttpContext.Session.SetString("AuthToken", content!.Token);
                return true;
            };

            return false;
        }

        // MÉTODO TESTE
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
