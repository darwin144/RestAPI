using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.ViewModels.Accounts;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account,AccountVM> _mapper;

        public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }
            var AccountConverteds = accounts.Select(_mapper.Map).ToList();
            return Ok(AccountConverteds);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return NotFound();
            }
            var accountConverted = _mapper.Map(account);
            return Ok(accountConverted);
        
        }
        [HttpPost]
        public IActionResult Create(AccountVM accountVM)
        {
            var account = _mapper.Map(accountVM);
            var result = _accountRepository.Create(account);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(AccountVM accountVM)
        {
            var account = _mapper.Map(accountVM);
            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _accountRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
