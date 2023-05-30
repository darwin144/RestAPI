using RestAPI.Utility;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.ViewModels.Accounts
{
    public class ChangePasswordVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public int OTP { get; set; }
        [PasswordValidationAttribut]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
