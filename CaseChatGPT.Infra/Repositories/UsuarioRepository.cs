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
        private readonly RoleManager<Role> _roleManager;
        private readonly BancoContext _context;
        public UsuarioRepository(UserManager<Usuario> userManager, BancoContext context, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
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
            try
            {

                if (string.IsNullOrWhiteSpace(usuario.UserName))
                {
                    throw new ArgumentException("O UserName não pode ser nulo ou vazio.");
                }

                var existe = await _userManager.FindByNameAsync(usuario.UserName);
                if (existe != null)
                {
                    throw new Exception("Esse nome de usuário já está em uso.");
                }

                var result = await _userManager.CreateAsync(usuario, passwordDTO);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    throw new Exception($"Erro ao criar usuário: {errors}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado ao criar usuário: {ex.Message}", ex);
            }
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

        public async Task<Role> GetRoleByUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);

            var role = await _roleManager.Roles
                .Where(r => userRoles.Contains(r.Name))
                .Select(r => new Role
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    NormalizedName = r.NormalizedName,
                    ConcurrencyStamp = r.ConcurrencyStamp
                })
                .FirstOrDefaultAsync();

            return role;
        }
    }
}
