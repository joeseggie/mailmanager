using MailManager.Models.MailViewModels;
using System.Collections.Generic;
using System;

namespace MailManager.Services
{
    public interface IOutgoingMail
    {
         IEnumerable<OutgoingMailViewModel> OutgoingMails { get; }
         OperationResult NewOutgoingMail(OutgoingMailViewModel newOutgoingMail);
         OperationResult EditOutgoingMail(OutgoingMailViewModel outgoingMailUpdate);
         OutgoingMailViewModel GetOutgoingMailById(Guid outgoingMailId);
         IEnumerable<OutgoingMailViewModel> GetOutgoingMailsByReferenceNumber(string referenceNumber);
    }
}