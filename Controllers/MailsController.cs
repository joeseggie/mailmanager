using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MailManager.Services;
using MailManager.Models.MailViewModels;

namespace MailManager.Controllers
{
    public class MailsController : Controller
    {
        private readonly IOfficeMail _mailService;
        private readonly IIncomingMail _incomingMailService;
        private readonly IOutgoingMail _outcomingMailService;

        public MailsController(IOfficeMail mailService, IIncomingMail incomingMailService, IOutgoingMail outgoingMailService)
        {
            _mailService = mailService;
            _incomingMailService = incomingMailService;
            _outcomingMailService = outgoingMailService;
        }

        public IActionResult Index(string search, string mailsearch, string filesearch)
        {
            var mails = _mailService.OfficeMails;
            if(search != null)
            {
                mails = mails.Where(m => m.ReferenceNumber.Contains(search)).ToList();
            }
            return View(mails);
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
            ModelState.Remove("ReferenceNumber");
            ModelState.Remove("RowVersion");
            ModelState.Remove("OfficeMail.ReferenceNumber");
            ModelState.Remove("OfficeMail.RowVersion");

            if(ModelState.IsValid)
            {
                newMail.ReferenceNumber = string.IsNullOrWhiteSpace(newMail.ReferenceNumber) ? _mailService.GenerateReferenceNumber() : newMail.ReferenceNumber;
                newMail.OfficeMail.ReferenceNumber = newMail.ReferenceNumber;

                var newOfficeMailOperation = _mailService.AddOfficeMail(newMail.OfficeMail);
                var operationResult = newOfficeMailOperation.Success ? _incomingMailService.NewIncomingMail(newMail) : newOfficeMailOperation;

                if(operationResult.Success)
                {
                    // TempData["Message"] = operationResult.Message;
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", operationResult.Message);
            }
            return View(newMail);
        }
    }
}