using System;
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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewActionStatusViewModel formData)
        {
            if (ModelState.IsValid)
            {
                var newActionStatus = await _actionStatusService.AddActionStatusAsync(new ActionStatus{
                    Description = formData.Description
                });

                return RedirectToAction("details", new{ id = newActionStatus.Id });
            }

            return View(formData);
        }

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