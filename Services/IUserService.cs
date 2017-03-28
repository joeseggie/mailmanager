using System.Collections.Generic;
using MailManager.Models.AccountViewModels;

namespace MailManager.Services
{
    public interface IUserService
    {
         IEnumerable<RegisterViewModel> Users { get; }
    }
}