using RestAPI.Model;

namespace RestAPI.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        Employee GetByEmployeeGuid(Guid bookingGuid);
    }
}
