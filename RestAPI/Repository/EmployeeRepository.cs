using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementContext context) : base(context)
        {
        }

        public Employee GetByEmployeeGuid(Guid bookingGuid)
        {
            var entity = _context.Set<Employee>().Find(bookingGuid);

            _context.ChangeTracker.Clear();
            return entity;
        }
    }
}
