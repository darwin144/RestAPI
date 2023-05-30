using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.Repository;
using RestAPI.Utility;
using RestAPI.ViewModels.Accounts;
using RestAPI.ViewModels.Register;
using RestAPI.ViewModels.Rooms;
using System.Security.Claims;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class AccountController : GeneralController<Account, AccountVM>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        public AccountController(ITokenService tokenService, IEmailService emailService, IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper, IEmployeeRepository employeeRepository) : base(accountRepository, mapper)
        {
            _emailService = emailService;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var respons = new ResponseVM<LoginVM>();
            try
            {
                var employee = _employeeRepository.FindEmployeeByEmail(loginVM.Email);
                var account = _accountRepository.Login(loginVM);

                if (account == null)
                {
                    return NotFound(ResponseVM<LoginVM>.NotFound(account));
                }

                var currentlyHash = Hashing.HashPassword(loginVM.Password);
                var validatePassword = Hashing.ValidatePassword(loginVM.Password, currentlyHash);
                //if (account.Password != loginVM.Password)
                if (!validatePassword)
                {
                    var message = "Password is invalid";
                    return NotFound(ResponseVM<string>.NotFound(message));
                }

                var claims = new List<Claim> {
                    new(ClaimTypes.NameIdentifier, employee.NIK),
                    new(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
                    new(ClaimTypes.Email, employee.Email)

                };

                var roles = _accountRepository.GetRoles(employee.Guid);

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = _tokenService.GenerateToken(claims);


                return Ok(ResponseVM<string>.Successfully(token));
            }
            catch(Exception ex) {

                return BadRequest(ResponseVM<string>.Error(ex.Message));            
                       
            }
        }
        [AllowAnonymous]
        [HttpPost("ForgotPassword/{email}")]
        public IActionResult UpdateResetPass(string email)
        {
            var respons = new ResponseVM<AccountResetPasswordVM>();
            try
            {
                
                var getGuid = _employeeRepository.FindGuidByEmail(email);
                if (getGuid == null)
                {
                    var message = "Akun tidak ditemukan";
                    return NotFound(ResponseVM<string>.NotFound(message));
                }

                var isUpdated = _accountRepository.UpdateOTP(getGuid);

                switch (isUpdated)
                {
                    case 0:
                        var message = "OTP belum di update";
                        return BadRequest(ResponseVM<string>.NotFound(message));

                    default:
                        var hasil = new AccountResetPasswordVM
                        {
                            Email = email,
                            OTP = isUpdated
                        };

                        _emailService.SetEmail(email)
                            .SetSubject("Forgot Password")
                            .SetHtmlMessage($"Your OTP is {hasil.OTP}")
                            .SentEmailAsynch();

                        return Ok(ResponseVM<AccountResetPasswordVM>.Successfully(hasil));
                }
            }
            catch (Exception ex) {
                return BadRequest(ResponseVM<string>.Error(ex.Message));
            }
        }
        [AllowAnonymous]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var respons = new ResponseVM<ChangePasswordVM>();
            try
            {
                // Cek apakah email dan OTP valid
                var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
                var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
                switch (changePass)
                {
                    case 0:
                        return BadRequest(ResponseVM<string>.Error("Runtime error"));
                    case 1:
                        return Ok(ResponseVM<string>.Successfully("Password has been changed successfully"));
                    case 2:
                        return BadRequest(ResponseVM<string>.Error("Invalid OTP"));
                    case 3:
                        return BadRequest(ResponseVM<string>.Error("OTP has already been used"));
                    case 4:
                        return BadRequest(ResponseVM<string>.Error("OTP expired"));
                    case 5:
                        return BadRequest(ResponseVM<string>.Error("Wrong Password No Same"));
                    default:
                        return BadRequest(ResponseVM<string>.Error("Runtime error"));
                }
                
            }
            catch (Exception ex) {
                return BadRequest(ResponseVM<string>.Error(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterVM registerVM)
        {
            var respons = new ResponseVM<RegisterVM>();
            
            try
            {
                var result = _accountRepository.Register(registerVM);
                switch (result)
                {
                    case 0:
                        return BadRequest(ResponseVM<string>.Error("Registration failed"));
                    case 1:
                        return BadRequest(ResponseVM<string>.Error("Email already exists"));
                    case 2:
                        return BadRequest(ResponseVM<string>.Error("Phone number already exists"));
                    case 3:
                        return Ok(ResponseVM<string>.Successfully("Registration success"));
                }

                return Ok(ResponseVM<string>.Successfully("Berhasil"));
            }
            catch(Exception ex) {
                return BadRequest(ResponseVM<string>.Error(ex.Message));
            }
        }

        [HttpGet("GetToken")]
        public IActionResult GetByToken(string token) {
            try
            {
                var claims = _tokenService.ExtractClaimsFromJwt(token);
                return Ok(ResponseVM<ClaimVM>.Successfully(claims));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseVM<ClaimVM>.Error(ex.Message));
            }
        }

    }
}
