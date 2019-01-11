using MailManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailManager.Web.Services
{
    public interface IActionStatusService
    {
        IQueryable<ActionStatus> GetActionStatuses();
        Task<ActionStatus> GetActionStatusAsync(Guid actionStatusId);
        Task<ActionStatus> UpdateActionStatusAsync(ActionStatus actionStatus);
        Task<ActionStatus> AddActionStatusAsync(ActionStatus actionStatus);
    }
}
