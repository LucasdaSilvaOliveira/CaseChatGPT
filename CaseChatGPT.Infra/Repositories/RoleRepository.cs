using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Infra.Context;
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
        public RoleRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var roles = await _bancoContext.Roles.ToListAsync();

            return roles;
        }

        public async Task<Role> GetRoleById(string id)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteRole(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetRoleByUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
