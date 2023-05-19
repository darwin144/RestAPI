using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUniversityRepository<Account> _accountRepository;

        public AccountController(IUniversityRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }

            return Ok(accounts);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        [HttpPost]
        public IActionResult Create(Account account)
        {

            var result = _accountRepository.Create(account);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(Account account)
        {
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
