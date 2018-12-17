using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MailManager.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IApplicationRolesService _applicationRolesService;

        public RolesController(
            ILogger<RolesController> logger,
            IApplicationRolesService applicationRolesService)
        {
            _logger = logger;
            _applicationRolesService = applicationRolesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _applicationRolesService
                .GetApplicationRoles()
                .OrderBy(r => r.Name)
                .Select(r => new RolesListViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();

            return View(model);
        }
    }
}