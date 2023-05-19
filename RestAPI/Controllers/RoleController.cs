using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IUniversityRepository<Role> _roleRepository;

        public RoleController(Contracts.IUniversityRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound();
            }

            return Ok(roles);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        [HttpPost]
        public IActionResult Create(Role role)
        {

            var result = _roleRepository.Create(role);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(Role role)
        {
            var isUpdated = _roleRepository.Update(role);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
