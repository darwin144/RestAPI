using RestAPI.Model;
using RestAPI.ViewModels.Employees;

namespace RestAPI.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        //Employee GetByEmployeeGuid(Guid bookingGuid);
        int CreateWithValidate(Employee employee);
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);
        public Guid? FindGuidByEmail(string email);
    }
}
