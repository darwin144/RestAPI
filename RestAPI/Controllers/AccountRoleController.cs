using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.ViewModels.AccontRole;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
        {
            _accountRoleRepository = accountRoleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return NotFound();
            }
            var accountRoleConverteds = accountRoles.Select(_mapper.Map).ToList();
            return Ok(accountRoleConverteds);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return NotFound();
            }
            var accountRoleConverted = _mapper.Map(accountRole);
            return Ok(accountRoleConverted);
        }
        [HttpPost]
        public IActionResult Create(AccountRoleVM accountRoleVM)
        {
            var accountRole = _mapper.Map(accountRoleVM);
            var result = _accountRoleRepository.Create(accountRole);
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(AccountRoleVM accountRoleVM)
        {
            var accountRole = _mapper.Map(accountRoleVM);
            var isUpdated = _accountRoleRepository.Update(accountRole);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRoleRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
