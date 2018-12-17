using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MailManager.Web.Services
{
    public class ApplicationRoleService : IApplicationRolesService
    {
        private readonly ApplicationDbContext _db;

        public ApplicationRoleService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add role.
        /// </summary>
        /// <param name="role">Role to be added.</param>
        /// <returns>Role added.</returns>
        public async Task<IdentityRole> AddRoleAsync(IdentityRole role)
        {
            var roleEntry = await _db.Roles.AddAsync(role);
            await _db.SaveChangesAsync();

            return roleEntry.Entity;
        }

        /// <summary>
        /// Get application role.
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Application role.</returns>
        public async Task<IdentityRole> GetApplicationRoleAsync(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name.ToLowerInvariant() == roleName.ToLowerInvariant());
        }

        /// <summary>
        /// Get application roles.
        /// </summary>
        /// <returns>Application roles.</returns>
        public IQueryable<IdentityRole> GetApplicationRoles()
        {
            return _db.Roles;
        }

        /// <summary>
        /// Update role.
        /// </summary>
        /// <param name="role">Role to update.</param>
        /// <returns>Role updated.</returns>
        public async Task<IdentityRole> UpdateRoleAsync(IdentityRole role)
        {
            var roleToUpdate = await _db.Roles.FirstOrDefaultAsync(r =>
                r.Id == role.Id);
            if (roleToUpdate == null)
            {
                throw new ApplicationException("Role to be updated was not found.");
            }

            roleToUpdate.Name = role.Name;

            _db.Update(roleToUpdate);
            await _db.SaveChangesAsync();

            return roleToUpdate;
        }
    }
}
