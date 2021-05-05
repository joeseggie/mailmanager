using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MailManager.Web.Controllers
{
    [Authorize(Roles = "Administrator, Support")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IApplicationRolesService _applicationRolesService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationUsersService _applicationUsersService;

        public RolesController(
            ILogger<RolesController> logger,
            IApplicationRolesService applicationRolesService,
            UserManager<ApplicationUser> userManager,
            IApplicationUsersService applicationUsersService,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _applicationRolesService = applicationRolesService;
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationUsersService = applicationUsersService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _applicationRolesService
                .GetApplicationRoles()
                .OrderBy(r => r.Name)
                .Select(r => new RolesListViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    NormalizedName = r.NormalizedName
                })
                .ToListAsync();

            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewRoleViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var roleNormalizedName = Regex.Replace(formData.Name, "\\W", "-").ToLower();
                var newRole = await _applicationRolesService.AddRoleAsync(new IdentityRole
                {
                    Name = formData.Name,
                    NormalizedName = roleNormalizedName
                });

                TempData["Message"] = "New roles added successfully";
                return RedirectToAction("index", "roles");
            }

            return View(formData);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var roleDetails = await _applicationRolesService.GetApplicationRoleAsync(id);
            if (roleDetails == null)
            {
                return NotFound();
            }

            var model = new RoleDetailsViewModel
            {
                Id = roleDetails.Id,
                Name = roleDetails.Name,
                NormalizedName = roleDetails.NormalizedName
            };

            model.Members = (await _userManager.GetUsersInRoleAsync(roleDetails.NormalizedName.ToLower()))
                .Select(u => new ApplicationUsersListViewModel
                {
                    Email = u.Email,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Username = u.UserName
                });

            ViewData["Users"] = await _applicationUsersService.GetApplicationUsers()
                .Select(u => new SelectListItem
                {
                    Text = $"{u.Firstname} {u.Lastname}",
                    Value = u.UserName
                }).ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(RoleDetailsViewModel formData)
        {
            ViewData["Users"] = await _applicationUsersService.GetApplicationUsers()
                .Select(u => new SelectListItem
                {
                    Text = $"{u.Firstname} {u.Lastname}",
                    Value = u.UserName
                }).ToListAsync();

            if (ModelState.IsValid)
            {
                var updatedRole = await _applicationRolesService.UpdateRoleAsync(new IdentityRole
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    NormalizedName = formData.NormalizedName
                });

                TempData["Message"] = "Changes saved successfully.";
                return RedirectToAction("details", new { id = formData.NormalizedName });
            }

            return View(formData);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _applicationRolesService.DeleteRoleAsync(id);

                TempData["Message"] = "Role delete successfully.";

                return RedirectToAction("index");
            }
            catch (ApplicationException)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Revoke(string username, string role)
        {
            var userAccount = await _applicationUsersService.GetApplicationUserAsync(username);
            var userRole = await _applicationRolesService.GetApplicationRoleAsync(role);
            var userRoleExists = await _roleManager.RoleExistsAsync(role);
            if (userRoleExists)
            {
                var result = await _userManager.RemoveFromRoleAsync(userAccount, role);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Successfully removed member from role";
                }
                else
                {
                    TempData["Message"] = "Failed to remove member from role";
                }
            }
            else
            {
                TempData["Message"] = "Role does not exist.";
            }

            return RedirectToAction("details", new { id = role });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(string role, string username)
        {


            var userAccount = await _applicationUsersService.GetApplicationUserAsync(username);
            var userRole = await _applicationRolesService.GetApplicationRoleAsync(role);
            var userRoleExists = await _roleManager.RoleExistsAsync(role);
            if (userRoleExists)
            {
                var result = await _userManager.AddToRoleAsync(userAccount, role);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Successfully added new member to role";
                }
                else
                {
                    TempData["Message"] = "Failed to add new member to role";
                }
            }
            else
            {
                TempData["Message"] = "Role does not exist.";
            }

            return RedirectToAction("details", new { id = role });
        }
    }
}