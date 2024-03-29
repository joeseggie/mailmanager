using MailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailManager.Web.Services
{
    public interface IApplicationUsersService
    {
        IQueryable<ApplicationUser> GetApplicationUsers();

        Task<ApplicationUser> GetApplicationUserAsync(string username);

        Task<ApplicationUser> UpdateUserAsync(ApplicationUser applicationUser);
    }
}