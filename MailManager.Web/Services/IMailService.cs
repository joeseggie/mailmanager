using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Models;

namespace MailManager.Web.Services
{
    public interface IMailService
    {
        Task<Mail> AddMailAsync(Mail mail);
        Task<Mail> UpdateMailAsync(Mail mail);
        Task<Mail> GetMailAsync(Guid mailId);
        IQueryable<Mail> GetMail();
    }
}