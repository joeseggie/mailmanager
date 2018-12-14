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
        private readonly IActionPointService _actionPointService;
        private readonly IMailService _mailService;
        private readonly ILogger<MailController> _logger;
        private readonly ICorrespondanceService _correspondanceService;

        public MailController(
            IMailService mailService,
            ILogger<MailController> logger,
            ICorrespondanceService correspondanceService,
            IActionPointService actionPointService
        )
        {
            _actionPointService = actionPointService;
            _mailService = mailService;
            _logger = logger;
            _correspondanceService = correspondanceService;
        }

        // GET: mail/
        public async Task<IActionResult> Index(string search, string sort, int page=1)
        {
            var model = await _mailService.GetMail()
                .OrderBy(m => m.Received)
                .Select(m => new MailListViewModel
                {
                    Id = m.Id,
                    ReferenceNumber = m.ReferenceNumber,
                    Details = m.Details,
                    From = m.From,
                    Received = m.Received.ToString("dd MMMM yyyy"),
                    Subject = m.Subject,
                    To = m.To
                }).ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model
                    .Where(m => 
                        m.From.ToLowerInvariant().Contains(search.ToLowerInvariant()) || 
                        m.To.ToLowerInvariant().Contains(search.ToLowerInvariant()) || 
                        m.Subject.ToLowerInvariant().Contains(search.ToLowerInvariant()))
                    .ToList();
                ViewData["search"] = search;
            }

            ViewData["FromSortParam"] = string.IsNullOrWhiteSpace(sort) ? "from_desc" : "";
            ViewData["ToSortParam"] = sort == "to" ? "to_desc" : "to";
            ViewData["SubjectSortParam"] = sort == "subject" ? "subject_desc" : "subject";
            switch (sort)
            {
                case "from_desc":
                    model = model.OrderByDescending(m => m.From).ToList();
                    ViewData["sort"] = "from";
                    break;
                case "to":
                    model = model.OrderBy(m => m.To).ToList();
                    ViewData["sort"] = "todesc";
                    break;
                case "to_desc":
                    model = model.OrderByDescending(m => m.To).ToList();
                    ViewData["sort"] = "to";
                    break;
                case "subject":
                    model = model.OrderBy(m => m.Subject).ToList();
                    ViewData["sort"] = "subjectdesc";
                    break;
                case "subject_desc":
                    model = model.OrderByDescending(m => m.Subject).ToList();
                    ViewData["sort"] = "subject";
                    break;
                default:
                    model = model.OrderBy(m => m.From).ToList();
                    break;
            }

            return View(model);
        }

        // GET mail/add
        public IActionResult Add()
        {
            return View();
        }

        // POST: mail/add
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var mailCorrespondances = await _correspondanceService.GetCorrespondances()
                .Where(c => c.MailId == mail.Id)
                .Select(c => new CorrespondanceListViewModel
                {
                    Details = c.Details,
                    Id = c.Id,
                    Logged = c.Logged.ToString("dd/MM/yyyy"),
                    MailId = c.MailId,
                    Office = c.Office
                }).ToListAsync();

            var mailActionPoints = await _actionPointService.GetActionPoints()
                .Where(c => c.MailId == mail.Id)
                .Select(a => new ActionPointListViewModel
                {
                    ActionStatus = a.ActionStatus.Description,
                    ActionStatusId = a.ActionStatusId,
                    Details = a.Details,
                    Id = a.Id
                }).ToListAsync();

            var model = new MailDetailsViewModel
            {
                Id = mail.Id,
                ReferenceNumber = mail.ReferenceNumber,
                Details = mail.Details,
                From = mail.From,
                Received = mail.Received.ToString("dd/MM/yyyy"),
                Subject = mail.Subject,
                To = mail.To,
                Correspondances = mailCorrespondances,
                ActionPoints = mailActionPoints
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