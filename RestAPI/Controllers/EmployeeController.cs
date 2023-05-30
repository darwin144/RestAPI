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
    public class EmployeeController : GeneralController<Employee, EmployeeVM>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
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
                    return NotFound(ResponseVM<MasterEmployeeVM>.NotFound(masterEmployees));
                }

                return Ok(ResponseVM<IEnumerable<MasterEmployeeVM>>.Successfully(masterEmployees));
            }
            catch (Exception ex) {
                return BadRequest(ResponseVM<string>.Error(ex.Message));
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
                    return NotFound(ResponseVM<MasterEmployeeVM>.NotFound(masterEmployees));
                }

                return Ok(ResponseVM<MasterEmployeeVM>.Successfully(masterEmployees));
            }
            catch (Exception ex) {
                return BadRequest(ResponseVM<string>.Error(ex.Message));
            }
        }

    }
}
