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

            ViewData["MailId"] = correspondanceMail.Id;
            return View();
        }

        [HttpPost("mail/{mail:guid}")]
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
    }
}