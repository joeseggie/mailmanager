using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MailManager.Services;

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
            mails = mails.Where(m => m.ReferenceNumber.Contains(search)).ToList();
            return View(mails);
        }
    }
}