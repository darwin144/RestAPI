using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.Repository;
using RestAPI.ViewModels.Employees;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class EmployeeController : GeneralController<Employee, EmployeeVM, IEmployeeRepository>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository,IGeneralRepository<Employee> generalRepository, IMapper<Employee, EmployeeVM> mapper) : base(generalRepository, mapper)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetAllMasterEmployee")]
        public IActionResult GetAllMasterEmployee()
        {
            var respons = new ResponseVM<IEnumerable<MasterEmployeeVM>>();
            try
            {
                var masterEmployees = _employeeRepository.GetAllMasterEmployee();
                if (!masterEmployees.Any())
                {
                    return NotFound(respons.NotFound(masterEmployees));
                }

                return Ok(respons.Success(masterEmployees));
            }
            catch (Exception ex) {
                return BadRequest(respons.Error(ex.Message));
            }
        }

        [HttpGet("GetMasterEmployeeByGuid")]
        public IActionResult GetMasterEmployeeByGuid(Guid guid)
        {
            var respons = new ResponseVM<MasterEmployeeVM>();
            try
            {
                var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
                if (masterEmployees is null)
                {
                    return NotFound(respons.NotFound(masterEmployees));
                }

                return Ok(respons.Success(masterEmployees));
            }
            catch (Exception ex) {
                return BadRequest(respons.Error(ex.Message));
            }
        }

    }
}
