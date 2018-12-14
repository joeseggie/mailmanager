using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MailManager.Web.Controllers
{
    public class CorrespondanceController : Controller
    {
        private readonly ILogger<CorrespondanceController> _logger;
        private readonly ICorrespondanceService _correspondanceService;
        private readonly IMailService _mailService;

        public CorrespondanceController(
            ICorrespondanceService correspondanceService,
            ILogger<CorrespondanceController> logger,
            IMailService mailService
        )
        {
            _logger = logger;
            _correspondanceService = correspondanceService;
            _mailService = mailService;
        }

        [HttpGet("mail/{mail:guid}/correspondances/add")]
        public async Task<IActionResult> Add(Guid mail)
        {
            var correspondanceMail = await _mailService.GetMailAsync(mail);
            if (correspondanceMail == null)
            {
                return NotFound();
            }

            var model = new NewCorrespondanceViewModel
            {
                MailId = correspondanceMail.Id
            };
            return View(model);
        }

        [HttpPost("mail/{mail:guid}/correspondances/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewCorrespondanceViewModel formData)
        {
            if (ModelState.IsValid)
            {
                if (DateTime.TryParse(formData.Logged, out DateTime dateLogged))
                {
                    var newCorrespondance = await _correspondanceService.AddCorreespondanceAsync(new Correspondance
                    {
                        Details = formData.Details,
                        Logged = dateLogged,
                        MailId = formData.MailId,
                        Office = formData.Office
                    });

                    TempData["Message"] = "Correspondance added successfully";
                    return RedirectToAction("details", new { controller = "mail", id = newCorrespondance.MailId });
                }

                ModelState.AddModelError("Logged", "Invalid date logged entered");
            }

            return View(formData);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var correspondanceToUpdate = await _correspondanceService.GetCorrespondanceAsync(id);
            if (correspondanceToUpdate == null)
            {
                return NotFound();
            }

            var model = new CorrespondanceDetailsViewModel
            {
                Details = correspondanceToUpdate.Details,
                Id = correspondanceToUpdate.Id,
                Logged = correspondanceToUpdate.Logged.ToString("dd/MM/yyyy"),
                MailId = correspondanceToUpdate.MailId,
                Office = correspondanceToUpdate.Office
            };

            return View(model);
        }

        [HttpPost("{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(CorrespondanceDetailsViewModel formData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (DateTime.TryParse(formData.Logged, out DateTime dateLogged))
                    {
                        var updatedCorrespondance = await _correspondanceService.UpdateCorrespondanceAsync(new Correspondance
                        {
                            Details = formData.Details,
                            Id = formData.Id,
                            Logged = dateLogged,
                            MailId = formData.MailId ?? Guid.Empty,
                            Office = formData.Office
                        });

                        TempData["Message"] = "Changes saved successfully";
                        return RedirectToAction("details", new { id = updatedCorrespondance.Id });
                    }

                    ModelState.AddModelError("Logged", "Invalid date logged entered");
                }
                catch (ApplicationException error)
                {
                    _logger.LogError(error.Message);
                    ModelState.AddModelError("", "Failed to updated correspondance details");
                }
            }

            return View(formData);
        }
    }
}