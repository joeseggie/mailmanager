using System;
using System.Collections.Generic;
using MailManager.Models;

namespace MailManager.Services
{
    public interface IIncomingMail
    {
         IEnumerable<IncomingMail> IncomingMails { get; }
         OperationResult NewIncomingMail(IncomingMail newMail);
         OperationResult EditIncomingMail(IncomingMail mailUpdate);
         IncomingMail GetIncomingMailById(Guid incomingMailId);
         IEnumerable<IncomingMail> GetIncomingMailsByReferenceNumber(string referenceNumber);
    }
}