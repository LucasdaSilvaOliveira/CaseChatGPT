using CaseChatGPT.Web.DTOs.Usuario;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CaseChatGPT.Web.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ObterUsuarioDTO> ObterUsuarioPorId(string id)
        {
            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.GetAsync($"Usuario/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var usuario = JsonSerializer.Deserialize<ObterUsuarioDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Task.FromResult(usuario);
                }
                else
                {
                    throw new Exception("Erro ao obter usuário.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public Task<List<ObterUsuarioDTO>> ObterUsuarios()
        {

            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.GetAsync("Usuario").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var usuarios = JsonSerializer.Deserialize<List<ObterUsuarioDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Task.FromResult(usuarios);
                }
                else
                {
                    throw new Exception("Erro ao obter usuários.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public Task<bool> RemoverUsuario(string id)
        {
            
            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.DeleteAsync($"Usuario/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    throw new Exception("Erro ao remover usuário.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public Task<ObterRoleUsuarioDTO> ObterRoleUsuarioPorId(string userId)
        {
            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.GetAsync($"Usuario/role/{userId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var role = JsonSerializer.Deserialize<ObterRoleUsuarioDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Task.FromResult(role);
                }
                else
                {
                    throw new Exception("Erro ao obter role do usuário.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }
    }
}
