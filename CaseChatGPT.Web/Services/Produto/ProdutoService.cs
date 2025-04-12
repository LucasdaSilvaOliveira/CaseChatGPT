using CaseChatGPT.Web.DTOs.Produto;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CaseChatGPT.Web.Services.Produto
{
    public class ProdutoService : IProdutoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProdutoService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ObterProdutoDTO>> ObterProdutos()
        {
            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("Produto");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var produtos = JsonSerializer.Deserialize<List<ObterProdutoDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return produtos;
                }
                else
                {
                    throw new Exception("Erro ao obter produtos.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public async Task<ObterProdutoDTO> ObterProdutoPorId(int id)
        {
            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"Produto/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var produto = JsonSerializer.Deserialize<ObterProdutoDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return produto;
                }
                else
                {
                    throw new Exception("Erro ao obter produto.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public async Task<List<ObterProdutoDTO>> ObterProdutosPorUserId(string userId)
        {

            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"Produto/ProdutoPorUsuario/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var produtos = JsonSerializer.Deserialize<List<ObterProdutoDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return produtos;
                }
                else
                {
                    throw new Exception("Erro ao obter produtos.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }

        }

        public async Task<bool> RemoverProduto(int id)
        {
            var hpptClientName = _configuration["HttpClientName"];
            var client = _httpClientFactory.CreateClient(hpptClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"Produto/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Erro ao remover produto.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public async Task<bool> AdicionarProduto(AdicionarProdutoDTO produto)
        {
            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsJsonAsync("Produto", produto);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Erro ao adicionar produto.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }

        public async Task<bool> AtualizarProduto(AtualizarProdutoDTO produto)
        {

            var httpClientName = _configuration["HttpClientName"]!;
            var client = _httpClientFactory.CreateClient(httpClientName);
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.PutAsJsonAsync("Produto", produto);

                if (response.Result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Erro ao atualizar produto.");
                }
            }
            else
            {
                throw new Exception("Token de autenticação não obtido.");
            }
        }
    }
}
