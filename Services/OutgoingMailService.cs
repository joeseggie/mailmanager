using System;
using System.Collections.Generic;
using MailManager.Models.MailViewModels;
using MailManager.Models;
using MailManager.Data;
using System.Linq;

namespace MailManager.Services
{
    public class OutgoingMailService : IOutgoingMail
    {
        private readonly ApplicationDbContext _db;

        public OutgoingMailService(ApplicationDbContext databaseContext)
        {
            _db = databaseContext;
        }

        public IEnumerable<OutgoingMailViewModel> OutgoingMails => _db.OutgoingMails
            .Select(m => new OutgoingMailViewModel{
                OutgoingMailId = m.OutgoingMailId,
                ReferenceNumber = m.ReferenceNumber,
                Comment = m.Comment,
                Officer = m.Officer,
                OutgoingDate = m.OutgoingDate,
                RowVersion = m.RowVersion
            });

        public OperationResult EditOutgoingMail(OutgoingMailViewModel outgoingMailUpdate)
        {
            throw new NotImplementedException();
        }

        public OutgoingMailViewModel GetOutgoingMailById(Guid outgoingMailId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OutgoingMailViewModel> GetOutgoingMailsByReferenceNumber(string referenceNumber)
        {
            throw new NotImplementedException();
        }

        public OperationResult NewOutgoingMail(OutgoingMailViewModel newOutgoingMail)
        {
            throw new NotImplementedException();
        }
    }
}