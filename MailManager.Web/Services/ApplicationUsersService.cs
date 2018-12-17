using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using MailManager.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MailManager.Web.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUsersService(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get application user given their username.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Application user profile.</returns>
        public async Task<ApplicationUser> GetApplicationUserAsync(string username)
        {
            return await _db
                .Users
                .SingleOrDefaultAsync(u => 
                    u.UserName.ToLowerInvariant() == username.ToLowerInvariant()
                );
        }

        /// <summary>
        /// Get all the users in the system.
        /// </summary>
        /// <returns>List of application users.</returns>
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return _db.Users;
        }

        /// <summary>
        /// Update application user details.
        /// </summary>
        /// <param name="applicationUser">Application user update details.</param>
        /// <returns>Application user.</returns>
        public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser applicationUser)
        {
            var userToUpdate = await _db
                .Users
                .SingleOrDefaultAsync(u =>
                    u.UserName.ToLowerInvariant() == applicationUser.UserName.ToLowerInvariant());

            if (userToUpdate == null)
            {
                throw new NullReferenceException("User was not found.");
            }

            userToUpdate.Firstname = applicationUser.Firstname;
            userToUpdate.Lastname = applicationUser.Lastname;

            _db.Update(userToUpdate);
            await _db.SaveChangesAsync();

            return userToUpdate;
        }
    }
}
