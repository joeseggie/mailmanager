using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MailManager.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IApplicationRolesService _applicationRolesService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUsersService _applicationUsersService;

        public HomeController(
            IApplicationRolesService applicationRolesService,
            UserManager<ApplicationUser> userManager,
            IApplicationUsersService applicationUsersService)
        {
            _applicationRolesService = applicationRolesService;
            _userManager = userManager;
            _applicationUsersService = applicationUsersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> InitialSetup()
        {
            IdentityRole adminRole;

            adminRole = await _applicationRolesService.GetApplicationRoles()
                .FirstOrDefaultAsync(r => r.NormalizedName == "administrator");

            if (adminRole == null)
            {
                adminRole = await _applicationRolesService.AddRoleAsync(new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "administrator"
                });
            }

            ApplicationUser adminAccount;

            adminAccount = await _applicationUsersService.GetApplicationUserAsync("admin");

            if (adminAccount == null)
            {
                adminAccount = new ApplicationUser
                {
                    Email = "admin@education.go.ug",
                    Firstname = "Admin",
                    Lastname = "Admin",
                    UserName = "admin"
                };
                var result = await _userManager.CreateAsync(adminAccount, "admin.education");

                if (!result.Succeeded)
                {
                    TempData["Message"] = "Initial setup configuration failed.";
                }
            }

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(adminAccount);
            var passwordResetResult = await _userManager.ResetPasswordAsync(adminAccount, passwordResetToken, "admin.education");

            if (passwordResetResult.Succeeded)
            {
                var isAdmin = await _userManager.IsInRoleAsync(adminAccount, "administrator");
                if (!isAdmin)
                {
                    var roleAssignmentResult = await _userManager.AddToRoleAsync(adminAccount, "administrator");
                    if (roleAssignmentResult.Succeeded)
                    {
                        TempData["Message"] = "Initial setup configuration completed. Use username: 'admin@education.go.ug' and password: 'admin.education' to login. PLEASE CHANGE PASSWORD AFTER LOGIN.";
                    }
                    else
                    {
                        TempData["Message"] = "Initial setup configuration failed.";
                    }
                }
                else
                {
                    TempData["Message"] = "Initial setup configuration completed. Use username: 'admin@education.go.ug' and password: 'admin.education' to login. PLEASE CHANGE PASSWORD AFTER LOGIN.";
                }
            }
            else
            {
                TempData["Message"] = "Initial setup configuration failed.";
            }

            return RedirectToAction("index", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
