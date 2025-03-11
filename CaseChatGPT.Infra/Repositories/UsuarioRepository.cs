using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly BancoContext _context;
        public UsuarioRepository(UserManager<Usuario> userManager, BancoContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> GetUsuarioById(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if(usuario == null) throw new Exception("Usuário não encontrado");
            return usuario;
        }

        public async Task AddUsuario(Usuario usuario, string passwordDTO)
        {
            var result = await _userManager.CreateAsync(usuario, passwordDTO);
            if(!result.Succeeded) throw new Exception("Erro ao criar usuário");
        }

        public async Task DeleteUsuario(Usuario usuario)
        {
            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded) throw new Exception("Erro ao deletar usuário");
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            var result = await _userManager.UpdateAsync(usuario);
            if (!result.Succeeded) throw new Exception("Erro ao atualizar usuário");
        }
    }
}
