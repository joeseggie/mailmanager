using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MailManager.Web.Controllers
{
    [Authorize(Roles = "Administrator, Support")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IApplicationUsersService _applicationUsersService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(
            ILogger<UsersController> logger,
            IApplicationUsersService applicationUsersService,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _applicationUsersService = applicationUsersService;
            _userManager = userManager;
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
        public async Task<IActionResult> Details(ApplicationUserDetailsViewModel userInput)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _applicationUsersService.GetApplicationUserAsync(userInput.Username);
                if (userToUpdate == null)
                {
                    ModelState.AddModelError("Username", "User was not found or does not exist.");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(userInput.Password))
                    {
                        if (userInput.Password.ToLowerInvariant() == userInput.ConfirmPassword.ToLowerInvariant())
                        {
                            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userToUpdate);
                            var passwordResetResult = await _userManager
                                .ResetPasswordAsync(
                                    userToUpdate,
                                    passwordResetToken,
                                    userInput.Password);
                            if (!passwordResetResult.Succeeded)
                            {
                                foreach (var error in passwordResetResult.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }
                                return View(userInput);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Please confirm new password to reset password.");
                            return View(userInput);
                        }
                    }

                    userToUpdate.Firstname = userInput.Firstname;
                    userToUpdate.Lastname = userInput.Lastname;

                    var updatedUser = await _applicationUsersService.UpdateUserAsync(userToUpdate);

                    TempData["Message"] = "Changes saved successfully";

                    return RedirectToAction("details", "users", new { username = updatedUser.UserName });
                }
            }

            return View(userInput);
        }
    }
}