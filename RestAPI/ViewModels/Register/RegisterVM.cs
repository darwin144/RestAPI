using RestAPI.Utility;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.ViewModels.Register
{
    public class RegisterVM
    {
        //public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }
        
        public GenderLevel Gender { get; set; }

        public DateTime HiringDate { get; set; }
        [EmailAddress]
        [NIKEmailPhoneValidationAttribut("Email")]
        public string Email { get; set; }
        [Phone]
        [NIKEmailPhoneValidationAttribut("PhoneNumber")]
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        [Range(0,4, ErrorMessage ="GPA must be between 0 and 4")]
        public float GPA { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        [PasswordValidationAttribut]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        // public University? University { get; set; }


    }
}
