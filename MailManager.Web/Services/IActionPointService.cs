using System;
using System.Linq;
using System.Threading.Tasks;
using MailManager.Web.Models;

namespace MailManager.Web.Services
{
    public interface IActionPointService
    {
        Task<ActionPoint> AddActionPointAsync(ActionPoint actionPoint);
        Task<ActionPoint> GetActionPointAsync(Guid actionPointId);
        IQueryable<ActionPoint> GetActionPoints();
        Task<ActionPoint> UpdateActionPointAsync(ActionPoint actionPoint);
    }
}