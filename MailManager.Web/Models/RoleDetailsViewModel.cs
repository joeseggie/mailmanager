using System.Collections.Generic;

namespace MailManager.Web.Models
{
    public class RoleDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public IEnumerable<ApplicationUsersListViewModel> Members { get; set; }
    }
}