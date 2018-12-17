using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailManager.Web.Services
{
    public interface IApplicationRolesService
    {

        IQueryable<IdentityRole> GetApplicationRoles();

        Task<IdentityRole> GetApplicationRoleAsync(string roleName);

        Task<IdentityRole> UpdateRoleAsync(IdentityRole role);

        Task<IdentityRole> AddRoleAsync(IdentityRole role)
    }
}
