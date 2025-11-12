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
    public class RoleRepository : IRoleRepository
    {
        private readonly BancoContext _bancoContext;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public RoleRepository(BancoContext bancoContext, UserManager<Usuario> userManager, RoleManager<Role> roleManager)
        {
            _bancoContext = bancoContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var roles = await _bancoContext.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleById(string id)
        {
            var role = await _bancoContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (role == null) throw new Exception("Role não encontrada");
            return role;
        }
        public async Task DeleteRole(string id)
        {
            var role = await _bancoContext.Roles.FirstOrDefaultAsync(_ => _.Id == id) ?? throw new Exception("Role não encontrado");
            _bancoContext.Roles.Remove(role);
            _bancoContext.SaveChanges();
        }

        public async Task UpdateRole(Role role)
        {
            if (string.IsNullOrEmpty(role.Id) ||
               string.IsNullOrEmpty(role.Name) ||
               string.IsNullOrEmpty(role.NormalizedName) ||
               string.IsNullOrEmpty(role.ConcurrencyStamp)) throw new Exception("Role não pode ser nulo");

            _bancoContext.Roles.Update(role);
            _bancoContext.SaveChanges();
        }

        // NÃO UTILIZADO NO MOMENTO - MÉTODO TAMBÉM SE ENCONTRA NO UsuarioRepository
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

            var breakP = false;

            return role;
        }
    }
}
