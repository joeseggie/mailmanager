using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Extensions;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MailManager.Web.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Index(
            string currentFilter,
            string search,
            string sort,
            int? view
        )
        {
            ViewData["CurrentSort"] = sort;
            var correspondances = _correspondanceService.GetCorrespondances()
                .Select(correspondance => new CorrespondanceListViewModel
                {
                    Details = correspondance.Details,
                    Id = correspondance.Id,
                    Logged = correspondance.Logged,
                    MailId = correspondance.MailId,
                    Office = correspondance.Office,
                    From = correspondance.Mail.From,
                    Subject = correspondance.Mail.Subject,
                    Received = correspondance.Mail.Received
                });

            if (!string.IsNullOrWhiteSpace(search))
            {
                correspondances = correspondances
                    .Where(correspondance =>
                        correspondance.Office.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                        correspondance.Details.ToLowerInvariant().Contains(search.ToLowerInvariant()));
                ViewData["search"] = search;
            }
            else
            {
                search = currentFilter;
            }

            ViewData["CurrentFilter"] = search;

            ViewData["LoggedSortParam"] = string.IsNullOrWhiteSpace(sort) ? "logged_desc" : "";
            ViewData["OfficeSortParam"] = sort == "office" ? "office_desc" : "office";
            switch (sort)
            {
                case "logged_desc":
                    correspondances = correspondances.OrderByDescending(m => m.Logged);
                    ViewData["sort"] = "logged";
                    break;
                case "office":
                    correspondances = correspondances.OrderBy(m => m.Office);
                    ViewData["sort"] = "office_desc";
                    break;
                case "office_desc":
                    correspondances = correspondances.OrderByDescending(m => m.Office);
                    ViewData["sort"] = "office";
                    break;
                default:
                    correspondances = correspondances.OrderBy(m => m.Logged);
                    break;
            }

            int pageSize = 10;

            return View(await PaginatedList<CorrespondanceListViewModel>.CreateAsync(correspondances.AsNoTracking(), view ?? 1, pageSize));
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

        [HttpGet("mail/{mail:guid}/correspondances/{id:guid}")]
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

        [HttpPost("mail/{mail:guid}/correspondances/{id:guid}")]
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

        public IActionResult PrintAll(string search)
        {
            var model = _correspondanceService.GetCorrespondances()
                .OrderByDescending(correspondance => correspondance.Logged)
                .Select(correspondance => new CorrespondanceListViewModel
                {
                    Details = correspondance.Details,
                    Id = correspondance.Id,
                    Logged = correspondance.Logged,
                    MailId = correspondance.MailId,
                    Office = correspondance.Office,
                    From = correspondance.Mail.From,
                    Subject = correspondance.Mail.Subject,
                    Received = correspondance.Mail.Received
                });

            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model
                    .Where(correspondance =>
                        correspondance.Office.ToLowerInvariant().Contains(search.ToLowerInvariant()) ||
                        correspondance.Details.ToLowerInvariant().Contains(search.ToLowerInvariant()));
            }

            return View(model);
        }
    }
}