using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class EmployeeRepository : IUniversityRepository<Employee>
    {
        private readonly BookingManagementContext _context;

        public EmployeeRepository(BookingManagementContext context)
        {
            _context = context;
        }

        public Employee Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch
            {
                return new Employee();
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var employee = GetByGuid(guid);
                if (employee is null)
                {
                    return false;
                }
                _context.Set<Employee>().Remove(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }

        public Employee GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid);
        }

        public bool Update(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Update(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
