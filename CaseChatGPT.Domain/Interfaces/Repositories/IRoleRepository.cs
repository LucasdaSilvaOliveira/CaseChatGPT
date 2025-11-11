using CaseChatGPT.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(string id);
        Task UpdateRole(Role role);
        Task DeleteRole(Role role);
        Task<Role> GetRoleByUserAsync(string userId);
    }
}
