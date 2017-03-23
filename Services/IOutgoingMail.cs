using MailManager.Models;
using System.Collections.Generic;
using System;

namespace MailManager.Services
{
    public interface IOutgoingMail
    {
         IEnumerable<OutgoingMail> OutgoingMails { get; }
         OperationResult NewOutgoingMail(OutgoingMail newOutgoingMail);
         OperationResult EditOutgoingMail(OutgoingMail outgoingMailUpdate);
         OutgoingMail GetOutgoingMailById(Guid outgoingMailId);
         IEnumerable<OutgoingMail> GetOutgoingMailsByReferenceNumber(string referenceNumber);
    }
}