using System.Collections.Generic;
using MailManager.Models.MailViewModels;

namespace MailManager.Services
{
    public interface IOfficeMail
    {
         IEnumerable<OfficeMailViewModel> OfficeMails { get; }
         OperationResult AddOfficeMail(OfficeMailViewModel newOfficeMail);
         OperationResult EditOfficeMail(OfficeMailViewModel officeMailUpdate);
         OfficeMailViewModel GetOfficeMailByReferenceNumber(string referenceNumber);
         string GenerateReferenceNumber();
    }
}