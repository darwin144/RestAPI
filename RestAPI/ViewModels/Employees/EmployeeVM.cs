using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestAPI.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.ViewModels.Employees
{
    public class EmployeeVM
    {
        public Guid? Guid { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
