﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly UserManager<IdentityUser> _userManager;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

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

            // Decodifica o JWT para extrair claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            // Criar claims do usuário
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("AuthToken", token), // Guarda o token como claim
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Autenticar o usuário na camada Web com Identity Cookies
            await _httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

            return true;
        }

        public class TokenResponse
        {
            public string Token { get; set; } = string.Empty;
        }
    }
}
