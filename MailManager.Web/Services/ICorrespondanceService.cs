using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Models;

namespace MailManager.Web.Services
{
    public interface ICorrespondanceService
    {
        Task<Correspondance> AddCorreespondanceAsync(Correspondance correspondance);
        Task<Correspondance> GetCorrespondanceAsync(Guid correspondanceId);
        IQueryable<Correspondance> GetCorrespondances();
        Task<Correspondance> UpdateCorrespondanceAsync(Correspondance correspondance);
    }
}