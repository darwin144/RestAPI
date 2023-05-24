using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Roles;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public readonly IMapper<Role,RoleVM> _mapper;

        public RoleController(IRoleRepository roleRepository, Contracts.IMapper<Role, RoleVM> mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound();
            }
            var roleConverted = roles.Select(_mapper.Map).ToList();

            return Ok(roleConverted);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return NotFound();
            }
            var roleConverted = _mapper.Map(role);
            return Ok(roleConverted);
        }
        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            var role = _mapper.Map(roleVM);
            var result = _roleRepository.Create(role);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(RoleVM roleVM)
        {
            var role = _mapper.Map(roleVM);
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
