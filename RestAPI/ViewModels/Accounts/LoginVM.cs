using System.ComponentModel.DataAnnotations;

namespace RestAPI.ViewModels.Accounts
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
