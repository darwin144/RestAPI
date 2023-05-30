using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestAPI.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.ViewModels.Employees
{
    public class EmployeeVM
    {
        public Guid? Guid { get; set; }
        [NIKEmailPhoneValidationAttribut(nameof(NIK))]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        [EmailAddress]
        [NIKEmailPhoneValidationAttribut (nameof(Email))]
        public string Email { get; set; }
        [Phone]
        [NIKEmailPhoneValidationAttribut (nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

    }
}
