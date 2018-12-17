using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MailManager.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IApplicationUsersService _applicationUsersService;

        public UsersController(ILogger<UsersController> logger, IApplicationUsersService applicationUsersService)
        {
            _logger = logger;
            _applicationUsersService = applicationUsersService;
        }

        public async Task<IActionResult> Details(string username)
        {
            var userDetails = await _applicationUsersService.GetApplicationUserAsync(username);
            if (userDetails == null)
            {
                return NotFound();
            }
            var model = new ApplicationUserDetailsViewModel
            {
                Firstname = userDetails.Firstname,
                Email = userDetails.Email,
                Lastname = userDetails.Lastname,
                Username = userDetails.Lastname
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ApplicationUserDetailsViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _applicationUsersService.GetApplicationUserAsync(formData.Username);
                if (userToUpdate == null)
                {
                    ModelState.AddModelError("Username", "User was not found or does not exist.");
                }
                else
                {
                    userToUpdate.Firstname = formData.Firstname;
                    userToUpdate.Lastname = formData.Lastname;

                    var updatedUser = await _applicationUsersService.UpdateUserAsync(userToUpdate);

                    TempData["Message"] = "Changes saved successfully";

                    return RedirectToAction("details", "users", new { username = updatedUser.UserName });
                }
            }

            return View(formData);
        }
    }
}