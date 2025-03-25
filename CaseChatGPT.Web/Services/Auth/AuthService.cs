using System.Net.Http;

namespace CaseChatGPT.Web.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(string username, string password)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7085/api/Pedido");

            if(response.IsSuccessStatusCode) return true;

            return false;
        }
    }
}
