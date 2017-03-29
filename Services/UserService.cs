using System;
using System.Collections.Generic;
using MailManager.Models.AccountViewModels;
using MailManager.Data;
using System.Linq;

namespace MailManager.Services
{    
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<RegisterViewModel> Users => _db.Users
            .Select(u => new RegisterViewModel{
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email,
                Username = u.UserName
            });
    }
}