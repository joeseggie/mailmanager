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
        /// Delete role.
        /// </summary>
        /// <param name="id">Role Id.</param>
        /// <returns>Task result.</returns>
        public async Task DeleteRoleAsync(string id)
        {
            var roleToDelete = await _db.Roles.FindAsync(id);
            if (roleToDelete == null)
            {
                throw new ApplicationException("Role to be deleted was not found.");
            }

            _db.Roles.Remove(roleToDelete);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Get application role.
        /// </summary>
        /// <param name="normalizedName">Role name</param>
        /// <returns>Application role.</returns>
        public async Task<IdentityRole> GetApplicationRoleAsync(string normalizedName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.NormalizedName.ToLowerInvariant() == normalizedName.ToLowerInvariant());
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
                r.NormalizedName == role.NormalizedName);
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