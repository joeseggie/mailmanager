using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Models;
using MailManager.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MailManager.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ActionStatusController : Controller
    {
        private readonly IActionStatusService _actionStatusService;
        private readonly ILogger<ActionStatusController> _logger;

        public ActionStatusController(
            IActionStatusService actionStatusService,
            ILogger<ActionStatusController> logger
        )
        {
            _actionStatusService = actionStatusService;
            _logger = logger;
        }

        // HTTPGET: actionstatus/?page=&sort=&search=
        public async Task<IActionResult> Index(string search, string sort, int page=1)
        {
            var model = await _actionStatusService.GetActionStatuses()
            .Select(s => new ActionStatusListViewModel{
                Id = s.Id,
                Description = s.Description
            }).ToListAsync();

            return View(model);
        }

        [Authorize(Roles = "Administrator, Support")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Support")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewActionStatusViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var newActionStatus = await _actionStatusService.AddActionStatusAsync(new ActionStatus{
                    Description = formData.Description
                });

                TempData["Message"] = "New action status added successfully";
                return RedirectToAction("details", new{ id = newActionStatus.Id });
            }

            return View(formData);
        }

        [Authorize(Roles = "Administrator, Support")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var actionStatus = await _actionStatusService.GetActionStatusAsync(id);
            if (actionStatus == null)
            {
                return NotFound();
            }

            var model = new ActionStatusDetailsViewModel{
                Id = actionStatus.Id,
                Description = actionStatus.Description
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator, Support")]
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Details(ActionStatusDetailsViewModel formData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedActionStatus = await _actionStatusService.UpdateActionStatusAsync(new ActionStatus{
                        Id = formData.Id,
                        Description = formData.Description
                    });

                    TempData["Message"] = "Changes saved successfully";
                    return RedirectToAction("details", new{ id = updatedActionStatus.Id });
                }
                catch (ApplicationException error)
                {
                    _logger.LogError(error.Message);
                    ModelState.AddModelError("", "Updating action status failed.");
                }
            }

            return View(formData);
        }
    }
}