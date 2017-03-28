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
            var outgoingMails = _outgoingMailService.OutgoingMails;
            if(!string.IsNullOrWhiteSpace(search))
            {
                outgoingMails = outgoingMails.Where(m => m.IncomingMail.Subject.ToLower().Contains(search.ToLower()));
            }
            outgoingMails.ToList();
            
            return View(outgoingMails);
        }

        public IActionResult New(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                TempData["Message"] = "No mail being responded to";
                return RedirectToAction("index", new { controller = "incomingmails" } );
            }
            Guid incomingMailId;
            if(Guid.TryParse(id, out incomingMailId))
            {
                ViewBag.IncomingMailId = incomingMailId;
                return View();
            }
            TempData["Message"] = "Invalid mail being responded to";
            return RedirectToAction("index", new { controller = "incomingmails" } );
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