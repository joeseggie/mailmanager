using System.Collections.Generic;
using MailManager.Models.AccountViewModels;
using MailManager.Data;
using System.Linq;
using MailManager.Models;
using System;

namespace MailManager.Services
{
    public class RoleService
    {
        private readonly ApplicationDbContext _db;

        public RoleService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }
    }
}