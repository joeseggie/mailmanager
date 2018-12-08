using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MailManager.Web.Controllers
{
    public class ActionPointController : Controller
    {
        private readonly IActionPointService _actionPointService;
        private readonly IMailService _mailService;
        private readonly ILogger<ActionPointController> _logger;
        private readonly IActionStatusService _actionStatusService;

        public ActionPointController(
            IActionPointService actionPointService,
            IMailService mailService,
            ILogger<ActionPointController> logger,
            IActionStatusService actionStatusService
        )
        {
            _actionPointService = actionPointService;
            _mailService = mailService;
            _logger = logger;
            _actionStatusService = actionStatusService;
        }

        [HttpGet("mail/{mail:guid}/actionpoints/add")]
        public async Task<IActionResult> Add(Guid mail)
        {
            var actionPointMail = await _mailService.GetMailAsync(mail);
            if (actionPointMail == null)
            {
                return NotFound();
            }

            ViewData["ActionStatuses"] = await _actionStatusService.GetActionStatuses()
                .Select(s => new SelectListItem
                {
                    Text = s.Description,
                    Value = s.Id.ToString()
                }).ToListAsync();

            ViewData["MailId"] = actionPointMail.Id;
            return View();
        }

        [HttpPost("mail/{mail:guid}/actionpoints/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewActionPointViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var newActionPoint = await _actionPointService.AddActionPointAsync(new ActionPoint
                {
                    ActionStatusId = formData.ActionStatusId,
                    Details = formData.Details,
                    MailId = formData.MailId
                });

                TempData["Message"] = "Action point added successfully";
                return RedirectToAction("details", new { controller = "mail", id = newActionPoint.MailId });
            }

            return View(formData);
        }
    }
}