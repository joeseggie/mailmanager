using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using MailManager.Services;
using MailManager.Models.MailViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace MailManager.Controllers
{
    [Authorize]
    public class IncomingMailsController : Controller
    {
        private readonly IIncomingMail _incomingMailService;
        private readonly ILogger<IncomingMailsController> _logger;

        public IncomingMailsController(IIncomingMail incomingMailService, ILogger<IncomingMailsController> logger)
        {
            _incomingMailService = incomingMailService;
            _logger = logger;
        }

        public IActionResult Index(string search)
        {
            _logger.LogInformation($"Accessing incoming mails with search term: {search??"None"}");
            var incomingMails = _incomingMailService.IncomingMails;
            if(!string.IsNullOrWhiteSpace(search))
            {
                incomingMails = incomingMails.Where(m => m.Subject.ToLower().Contains(search.ToLower()) || m.From.ToLower().Contains(search.ToLower()));
            }
            incomingMails.ToList();
            ViewBag.search = search;
            
            return View(incomingMails);
        }

        public IActionResult Sender(string id)
        {
            _logger.LogInformation($"Accessing incoming mails from sender: {id??"None"}");
            var incomingMails = _incomingMailService.IncomingMails;
            if(!string.IsNullOrWhiteSpace(id))
            {
                incomingMails = incomingMails.Where(m => m.From.ToLower().Replace(' ', '-') == id.ToLower());
            }
            incomingMails.ToList();
            
            return View(incomingMails);
        }

        public IActionResult New()
        {
            _logger.LogInformation($"Accessing view to add new incoming mail");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(IncomingMailViewModel newMail)
        {
            _logger.LogInformation($"Saving new incoming mail");
            ModelState.Remove("IncomingMailId");
            ModelState.Remove("RowVersion");

            if(ModelState.IsValid)
            {
                var newIncomingMailOperationResult = _incomingMailService.NewIncomingMail(newMail);
                if(newIncomingMailOperationResult.Success)
                {
                    TempData["Message"] = newIncomingMailOperationResult.Message;
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", newIncomingMailOperationResult.Message);
            }

            return View(newMail);
        }

        public IActionResult Details(string id)
        {
            _logger.LogInformation($"Accessing details of incomingmail with Id: {id??"None"}");
            if(string.IsNullOrWhiteSpace(id))
            {
                TempData["Message"] = "Incoming mail Id not supplied";
            }
            else
            {
                Guid incomingMailId;
                if(Guid.TryParse(id, out incomingMailId))
                {
                    var viewModel = _incomingMailService.GetIncomingMailById(incomingMailId);
                    return View(viewModel);
                }
                TempData["Message"] = "Invalid incoming mail Id supplied";
            }
            
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(IncomingMailViewModel mailUpdates)
        {
            _logger.LogInformation($"Updating details of incomingmail with Id: {mailUpdates.IncomingMailId.ToString()??"None"}");
            if(ModelState.IsValid)
            {
                var mailUpdateOperationResult = _incomingMailService.EditIncomingMail(mailUpdates);
                if(mailUpdateOperationResult.Success)
                {
                    TempData["Message"] = mailUpdateOperationResult.Message;
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", mailUpdateOperationResult.Message);
            }
            return View(mailUpdates);
        }
    }
}