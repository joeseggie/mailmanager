using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using MailManager.Services;
using MailManager.Models.MailViewModels;

namespace MailManager.Controllers
{
    public class OutgoingMailsController : Controller
    {
        private readonly IOutgoingMail _outgoingMailService;

        public OutgoingMailsController(IOutgoingMail outgoingMailService)
        {
            _outgoingMailService = outgoingMailService;
        }

        public IActionResult Index(string search)
        {
            var model = _outgoingMailService.OutgoingMails;
            if(search != null)
            {
                model = model.Where(m => m.IncomingMail.ReferenceNumber.ToLower().Contains(search.ToLower()) || m.IncomingMail.Subject.ToLower().Contains(search.ToLower()));
            }
            return View(model.ToList());
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(OutgoingMailViewModel newMail)
        {
            ModelState.Remove("RowVersion");

            if(ModelState.IsValid)
            {
                var newOutgoingMailOperationResult = _outgoingMailService.NewOutgoingMail(newMail);
                if(newOutgoingMailOperationResult.Success)
                {
                    TempData["Message"] = newOutgoingMailOperationResult.Message;
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", newOutgoingMailOperationResult.Message);
            }

            return View(newMail);
        }

        public IActionResult Details(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                TempData["Message"] = "Outgoing mail Id not supplied";
            }
            else
            {
                Guid incomingMailId;
                if(Guid.TryParse(id, out incomingMailId))
                {
                    var viewModel = _outgoingMailService.GetOutgoingMailById(incomingMailId);
                    return View(viewModel);
                }
                TempData["Message"] = "Invalid outgoing mail Id supplied";
            }
            
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(OutgoingMailViewModel mailUpdates)
        {
            if(ModelState.IsValid)
            {
                var mailUpdateOperationResult = _outgoingMailService.EditOutgoingMail(mailUpdates);
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