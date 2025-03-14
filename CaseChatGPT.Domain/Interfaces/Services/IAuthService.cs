using CaseChatGPT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(Usuario user);
        Task<Usuario> AuthenticateAsync(string email, string password);
        Task<string> LoginAsync(string userName, string email, string password);
    }
}
