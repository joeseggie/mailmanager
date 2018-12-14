using System.ComponentModel.DataAnnotations;

namespace MailManager.Web.Models
{
    public class NewActionStatusViewModel
    {
        [Required, Display(Name = "Action status")]
        public string Description { get; set; }
    }
}