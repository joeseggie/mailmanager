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
    [Route("[controller]/[action]")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ILogger<MailController> _logger;

        public MailController(IMailService mailService, ILogger<MailController> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }

        // GET: mail/
        public async Task<IActionResult> Index(string search, string sort, int page=1)
        {
            var model = await _mailService.GetMail()
                .Select(m => new MailListViewModel
                {
                    Id = m.Id,
                    ReferenceNumber = m.ReferenceNumber,
                    Details = m.Details,
                    From = m.From,
                    Received = m.Received,
                    Subject = m.Subject,
                    To = m.To
                }).ToListAsync();
            return View(model);
        }

        // GET mail/add
        public IActionResult Add()
        {
            return View();
        }

        // POST: mail/add
        public async Task<IActionResult> Add(NewMailViewModel formData)
        {
            if (ModelState.IsValid)
            {
                if (DateTime.TryParse(formData.Received, out DateTime dateReceived))
                {
                    var newMail = await _mailService.AddMailAsync(new Mail
                    {
                        ReferenceNumber = formData.ReferenceNumber,
                        Details = formData.Details,
                        From = formData.From,
                        To = formData.To,
                        Subject = formData.Subject,
                        Received = dateReceived
                    });

                    TempData["Message"] = "Incoming mail logged successfully.";
                    return RedirectToAction("details", new { id = newMail.Id });
                }

                ModelState.AddModelError("Received", "Invalid date received entered.");
            }

            return View(formData);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var mail = await _mailService.GetMailAsync(id);
            if (mail == null)
            {
                return NotFound();
            }

            var model = new MailDetailsViewModel
            {
                Id = mail.Id,
                ReferenceNumber = mail.ReferenceNumber,
                Details = mail.Details,
                From = mail.From,
                Received = mail.Received.ToString("dd/MM/yyyy"),
                Subject = mail.Subject,
                To = mail.To
            };

            return View(model);
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Details(MailDetailsViewModel formData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (DateTime.TryParse(formData.Received, out DateTime dateReceived))
                    {
                        var updatedMail = await _mailService.UpdateMailAsync(new Mail
                        {
                            Details = formData.Details,
                            From = formData.From,
                            Id = formData.Id,
                            Received = dateReceived,
                            ReferenceNumber = formData.ReferenceNumber,
                            Subject = formData.Subject,
                            To = formData.To
                        });

                        TempData["Message"] = "Changes saved successfully.";
                        return RedirectToAction("details", new { id = updatedMail.Id });
                    }

                    ModelState.AddModelError("Received", "Invalid date received entered");
                }
                catch (ApplicationException error)
                {
                    _logger.LogError(error.Message);
                    ModelState.AddModelError("", "Updating received mail details failed");
                }
            }

            return View(formData);
        }
    }
}