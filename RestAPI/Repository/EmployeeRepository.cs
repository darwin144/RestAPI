using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Employees;
using System.Linq;

namespace RestAPI.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementContext context) : base(context)
        {
        }

        public bool CheckEmailAndPhone(string value) {
            /*var email = _context.Employees.Any(e => e.Email == value);
            var phone = _context.Employees.Any(e => e.PhoneNumber == value);
            */
            return _context.Employees.Any(e => e.NIK == value && e.NIK == value && e.NIK == value);
            
            
        }
        public Guid? FindGuidByEmail(string email)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
                if (employee == null)
                {
                    return null;
                }
                return employee.Guid;
            }
            catch
            {
                return null;
            }
        }

        public int CreateWithValidate(Employee employee)
        {
            try
            {
                bool ExistsByEmail = _context.Employees.Any(e => e.Email == employee.Email);
                if (ExistsByEmail)
                {
                    return 1;
                }

                bool ExistsByPhoneNumber = _context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);
                if (ExistsByPhoneNumber)
                {
                    return 2;
                }

                Create(employee);
                return 3;

            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
        {
            var employees = GetAll();
            var educations = _context.Educations.ToList();
            var universities = _context.Universities.ToList();

            var employeeEducations = new List<MasterEmployeeVM>();

            foreach (var employee in employees)
            {
                var education = educations.FirstOrDefault(e => e.Guid == employee?.Guid);
                var university = universities.FirstOrDefault(u => u.Guid == education?.UniversityGuid);

                if (education != null && university != null)
                {
                    var employeeEducation = new MasterEmployeeVM
                    {
                        Guid = employee.Guid,
                        NIK = employee.NIK,
                        FullName = employee.FirstName + " " + employee.LastName,
                        BirthDate = employee.BirthDate,
                        Gender = employee.Gender.ToString(),
                        HiringDate = employee.HiringDate,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        Major = education.Major,
                        Degree = education.Degree,
                        GPA = education.GPA,
                        UniversityName = university.Name
                    };

                    employeeEducations.Add(employeeEducation);
                }
            }
            return employeeEducations;
        }
        MasterEmployeeVM? IEmployeeRepository.GetMasterEmployeeByGuid(Guid guid)
        {
            var employee = GetByGuid(guid);
            var education = _context.Educations.Find(guid);
            var university = _context.Universities.Find(education.UniversityGuid);

            var data = new MasterEmployeeVM
            {
                Guid = employee.Guid,
                NIK = employee.NIK,
                FullName = employee.FirstName + " " + employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender.ToString(),
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                GPA = education.GPA,
                UniversityName = university.Name
            };

            return data;
        }

        public Employee GetByEmployeeGuid(Guid bookingGuid)
        {
            var entity = _context.Set<Employee>().Find(bookingGuid);

            _context.ChangeTracker.Clear();
            return entity;
        }

        public Employee FindEmployeeByEmail(string email)
        {        

            return _context.Set<Employee>().FirstOrDefault(a => a.Email == email);
        }
    }
}
