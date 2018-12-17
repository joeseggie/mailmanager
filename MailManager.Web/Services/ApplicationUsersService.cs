using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Data;
using MailManager.Web.Models;

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
        /// Get all the users in the system.
        /// </summary>
        /// <returns>List of application users.</returns>
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return _db.Users;
        }
    }
}
