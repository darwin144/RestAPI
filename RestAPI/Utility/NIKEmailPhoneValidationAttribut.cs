using RestAPI.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Utility
{
    public class NIKEmailPhoneValidationAttribut : ValidationAttribute
    {
        private readonly string _propertyName;

        public NIKEmailPhoneValidationAttribut(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"{_propertyName} is required.");
            var employeeRepository = validationContext.GetService(typeof(IEmployeeRepository))
                                        as IEmployeeRepository;
            
            var checkEmailAndPhone = employeeRepository.CheckEmailAndPhone(value.ToString());
            if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
            return ValidationResult.Success;
        }
    }
}
