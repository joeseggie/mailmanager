using System.Collections.Generic;
using MailManager.Models;

namespace MailManager.Services
{
    public interface IOfficeMail
    {
         IEnumerable<OfficeMail> OfficeMails { get; }
         OperationResult AddOfficeMail(OfficeMail newOfficeMail);
         OperationResult EditOfficeMail(OfficeMail officeMailUpdate);
         OfficeMail GetOfficeMailByReferenceNumber(string referenceNumber);
         string GenerateReferenceNumber();
    }
}