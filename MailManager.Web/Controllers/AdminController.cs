using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Extensions;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MailManager.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IApplicationUsersService _applicationUsersService;

        public AdminController(
            ILogger<AdminController> logger,
            IApplicationUsersService applicationUsersService)
        {
            _logger = logger;
            _applicationUsersService = applicationUsersService;
        }

        public async Task<IActionResult> Index(int? view)
        {
            var model = _applicationUsersService
                .GetApplicationUsers()
                .OrderBy(u => u.Firstname)
                .Select(u => new ApplicationUsersListViewModel
                {
                    Email = u.Email,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Username = u.UserName
                });

            int pageSize = 10;

            return View(
                await PaginatedList<ApplicationUsersListViewModel>
                .CreateAsync(
                    model.AsNoTracking(), view ?? 1, pageSize));
        }
    }
}