using MailManager.Data;
using System;
using System.Collections.Generic;
using MailManager.Models.MailViewModels;
using System.Linq;
using MailManager.Models;

namespace MailManager.Services
{
    public class OfficeMailService : IOfficeMail
    {
        private readonly ApplicationDbContext _db;

        public OfficeMailService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<OfficeMailViewModel> OfficeMails => _db.OfficeMails
            .Select(m => new OfficeMailViewModel{
                ReferenceNumber = m.ReferenceNumber,
                Subject = m.Subject,
                From = m.From,
                To = m.To,
                RowVersion = m.RowVersion
            });

        public OperationResult AddOfficeMail(OfficeMailViewModel newOfficeMail)
        {
            _db.OfficeMails.Add(new OfficeMail{
                ReferenceNumber = newOfficeMail.ReferenceNumber,
                Subject = newOfficeMail.Subject,
                From = newOfficeMail.From,
                To = newOfficeMail.To
            });
            _db.SaveChanges();

            return new OperationResult{ Success = true, Message = "Mail added successfully" };
        }

        public OperationResult EditOfficeMail(OfficeMailViewModel officeMailUpdate)
        {
            var mailForUpdate = _db.OfficeMails.Find(officeMailUpdate.ReferenceNumber);
            
            if(mailForUpdate == null)
                return new OperationResult{ Success = false, Message = "Mail for update doesn't exist" };

            mailForUpdate.Subject = officeMailUpdate.Subject;
            mailForUpdate.From = officeMailUpdate.From;
            mailForUpdate.To = officeMailUpdate.To;

            _db.Update(mailForUpdate);
            _db.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Mail updated successfully" };
        }

        public string GenerateReferenceNumber()
        {
            Random generator = new Random();
            return $@"MOES\{generator.Next(1, 99).ToString("D3")}";
        }

        public OfficeMailViewModel GetOfficeMailByReferenceNumber(string referenceNumber) => OfficeMails
            .SingleOrDefault(m => m.ReferenceNumber == referenceNumber);
    }
}