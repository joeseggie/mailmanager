using System;
using System.Collections.Generic;
using MailManager.Models.MailViewModels;

namespace MailManager.Services
{
    public interface IIncomingMail
    {
         IEnumerable<IncomingMailViewModel> IncomingMails { get; }
         OperationResult NewIncomingMail(IncomingMailViewModel newMail);
         OperationResult EditIncomingMail(IncomingMailViewModel mailUpdate);
         IncomingMailViewModel GetIncomingMailById(Guid incomingMailId);
    }
}