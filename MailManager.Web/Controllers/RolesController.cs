﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Identity;
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
                var roleNormalizedName = Regex.Replace(formData.Name, "\\W", "-").ToLowerInvariant();
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(RoleDetailsViewModel formData)
        {
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
    }
}