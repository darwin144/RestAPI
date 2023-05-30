using RestAPI.Utility;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.ViewModels.Employees
{
    public class MasterEmployeeVM
    {       
            public Guid? Guid { get; set; }
            [Required]    
            [NIKEmailPhoneValidationAttribut(nameof(NIK))]
            public string NIK { get; set; }
            public string FullName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Gender { get; set; }
            public DateTime HiringDate { get; set; }
            [EmailAddress]
            [NIKEmailPhoneValidationAttribut(nameof(Email))]
            public string Email { get; set; }
            [Phone]
            [NIKEmailPhoneValidationAttribut(nameof(PhoneNumber))]
            public string PhoneNumber { get; set; }
            public string Major { get; set; }
            public string Degree { get; set; }
            [Range(0,4, ErrorMessage ="Must be 0 - 4")]
            public float GPA { get; set; }
            public string UniversityName { get; set; }
        
    }
}
