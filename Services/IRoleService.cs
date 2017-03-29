using System.Collections.Generic;
using MailManager.Models;

namespace MailManager.Services
{
    public interface IRoleService
    {
         IEnumerable<ApplicationRole> Roles { get; }
    }
}