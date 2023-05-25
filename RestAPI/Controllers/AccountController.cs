using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.Repository;
using RestAPI.Utility;
using RestAPI.ViewModels.Accounts;
using RestAPI.ViewModels.Register;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class AccountController : GeneralController<Account, AccountVM, IAccountRepository>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        

        public AccountController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IGeneralRepository<Account> generalRepository, IMapper<Account, AccountVM> mapper) : base(generalRepository, mapper)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
           
        }

        [HttpPost("login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var respons = new ResponseVM<LoginVM>();
            try
            {
                var account = _accountRepository.Login(loginVM);

                if (account == null)
                {
                    return NotFound(respons.NotFound(account));
                }

                if (account.Password != loginVM.Password)
                {
                    var message = "Password is invalid";
                    return NotFound(respons.NotFound(message));
                }

                return Ok(respons.Success(account));
            }
            catch(Exception ex) {

                return BadRequest(respons.Error(ex.Message));            
                       
            }
        }

        [HttpPost("ForgotPassword" + "{email}")]
        public IActionResult UpdateResetPass(String email)
        {
            var respons = new ResponseVM<AccountResetPasswordVM>();
            try
            {
                var getGuid = _employeeRepository.FindGuidByEmail(email);
                if (getGuid == null)
                {
                    var message = "Akun tidak ditemukan";
                    return NotFound(respons.NotFound(message));
                }

                var isUpdated = _accountRepository.UpdateOTP(getGuid);

                switch (isUpdated)
                {
                    case 0:
                        var message = "OTP belum di update";
                        return BadRequest(respons.NotFound(message));

                    default:
                        var hasil = new AccountResetPasswordVM
                        {
                            Email = email,
                            OTP = isUpdated
                        };

                        MailService mailService = new MailService();
                        mailService.WithSubject("Kode OTP")
                                   .WithBody("OTP anda adalah: " + isUpdated.ToString() + ".\n" +
                                             "Mohon kode OTP anda tidak diberikan kepada pihak lain" + ".\n" + "Terima kasih.")
                                   .WithEmail(email)
                                   .Send();

                        return Ok(respons.Success(hasil));

                }
            }
            catch (Exception ex) {
                return BadRequest(respons.Error(ex.Message));
            }
        }

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
                        return BadRequest(respons.Error("Runtime error"));
                    case 1:
                        return Ok(respons.Success("Password has been changed successfully"));
                    case 2:
                        return NotFound(respons.Error("Invalid OTP"));
                    case 3:
                        return NotFound(respons.Error("OTP has already been used"));
                    case 4:
                        return NotFound(respons.Error("OTP expired"));
                    case 5:
                        return BadRequest(respons.Error("Wrong Password No Same"));
                    default:
                        return BadRequest(respons.Error("Runtime error"));
                }
                
            }
            catch (Exception ex) {
                return BadRequest(respons.Error(ex.Message));
            }
        }


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
                        return BadRequest(respons.NotFound("Registration failed"));
                    case 1:
                        return BadRequest(respons.NotFound("Email already exists"));
                    case 2:
                        return BadRequest(respons.NotFound("Phone number already exists"));
                    case 3:
                        return Ok(respons.Success("Registration success"));
                }

                return Ok(respons.Success("Berhasil"));
            }
            catch(Exception ex) {
                return BadRequest(respons.Error(ex.Message));
            }
        }


    }
}
