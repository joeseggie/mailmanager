using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using MailManager.Services;
using MailManager.Models.MailViewModels;

namespace MailManager.Controllers
{
    public class IncomingMailsController : Controller
    {
        private readonly IIncomingMail _incomingMailService;

        public IncomingMailsController(IIncomingMail incomingMailService)
        {
            _incomingMailService = incomingMailService;
        }

        public IActionResult Index(string search)
        {
            var incomingMails = _incomingMailService.IncomingMails;
            if(!string.IsNullOrWhiteSpace(search))
            {
                incomingMails = incomingMails.Where(m => m.Subject.ToLower().Contains(search.ToLower()));
            }
            incomingMails.ToList();
            
            return View(incomingMails);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(IncomingMailViewModel newMail)
        {
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