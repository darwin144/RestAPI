using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUniversityRepository<Employee> _employeeRepository;

        public EmployeeController(IUniversityRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return NotFound();
            }

            return Ok(employees);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            var result = _employeeRepository.Create(employee);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            var isUpdated = _employeeRepository.Update(employee);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _employeeRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
