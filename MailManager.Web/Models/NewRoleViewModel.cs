using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class NewRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}