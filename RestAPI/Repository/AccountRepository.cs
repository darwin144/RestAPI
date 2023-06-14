using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Utility;
using RestAPI.ViewModels.AccontRole;
using RestAPI.ViewModels.Accounts;
using RestAPI.ViewModels.Register;
//using System.Data;

namespace RestAPI.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;

        public AccountRepository( BookingManagementContext context, IUniversityRepository universityRepository,
                            IEmployeeRepository employeeRepository, IEducationRepository educationRepository) 
        : base(context)
        {
            _universityRepository = universityRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
        }

        public int UpdateOTP(Guid? employeeId)
        {
            var account = new Account();
            //account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            account = _context.Accounts.Find(employeeId);
            //Generate OTP
            Random rnd = new Random();
            var getOtp = rnd.Next(100000, 999999);
            account.OTP = getOtp;

            //Add 5 minutes to expired time
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            try
            {
                var check = Update(account);


                if (!check)
                {
                    return 0;
                }
                return getOtp;
            }
            catch
            {
                return 0;
            }
        }
        public LoginVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var employee = _employeeRepository.GetAll();


            var query = from emp in employee
                        join acc in account
                        on emp.Guid equals acc.Guid
                        where emp.Email == loginVM.Email 
                        select new LoginVM
                        {
                            Email = emp.Email,
                            Password = acc.Password

                        };

            var findAccount = query.FirstOrDefault();
            return findAccount;
        }
        public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            if (account == null || account.OTP != changePasswordVM.OTP)
            {
                return 2;
            }
            // Cek apakah OTP sudah digunakan
            if (account.IsUsed)
            {
                return 3;
            }
            // Cek apakah OTP sudah expired
            if (account.ExpiredTime < DateTime.Now)
            {
                return 4;
            }
            // Cek apakah NewPassword dan ConfirmPassword sesuai
            if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
            {
                return 5;
            }
            // Update password
            account.Password = changePasswordVM.NewPassword;
            account.IsUsed = true;
            try
            {
                var updatePassword = Update(account);
                if (!updatePassword)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        private string GenerateNIK()
        {
            var lastNik = _employeeRepository.GetAll().OrderByDescending(e => Convert.ToInt32(e.NIK)).FirstOrDefault();

            if (lastNik != null)
            {
                int lastNikNumber;
                if (int.TryParse(lastNik.NIK, out lastNikNumber))
                {
                    return (lastNikNumber + 1).ToString();
                }
            }

            return "100000";
        }
        public int Register(RegisterVM registerVM)
        {          
                try
                {
                    var university = new University
                    {
                        Code = registerVM.Code,
                        Name = registerVM.Name

                    };
                    _universityRepository.CreateWithValidate(university);

                    var employee = new Employee
                    {
                        NIK = GenerateNIK(),
                        FirstName = registerVM.FirstName,
                        LastName = registerVM.LastName,
                        BirthDate = registerVM.BirthDate,
                        Gender = registerVM.Gender,
                        HiringDate = registerVM.HiringDate,
                        Email = registerVM.Email,
                        PhoneNumber = registerVM.PhoneNumber,
                    };
                    var result = _employeeRepository.CreateWithValidate(employee);

                    if (result != 3)
                    {
                        return result;
                    }

                    var education = new Education
                    {
                        Guid = employee.Guid,
                        Major = registerVM.Major,
                        Degree = registerVM.Degree,
                        GPA = registerVM.GPA,
                        UniversityGuid = university.Guid
                    };
                    _educationRepository.Create(education);

                    //hashing password
                    registerVM.Password = Hashing.HashPassword(registerVM.Password);
                    var account = new Account
                    {
                        Guid = employee.Guid,
                        Password = registerVM.Password,
                        IsDeleted = false,
                        IsUsed = true,
                        OTP = 0
                    };

                    Create(account);

                    var accountRole = new AccountRole {
                        RoleGuid = Guid.Parse("f147a695-1a4f-4960-bffc-08db60bf618f"),
                        AccountGuid = employee.Guid
                     };
                    _context.AccountRoles.Add(accountRole);
                    _context.SaveChanges();

                    return 3;

                }
                catch
                {
                    return 0;
                }
            
        }

        public IEnumerable<string> GetRoles(Guid guid)
        {
            var account = GetByGuid(guid);
            if (account is null) {
                return Enumerable.Empty<string>();
            }
            var Accountroles = from accounts in _context.AccountRoles
                        join roles in _context.Roles on accounts.Guid equals roles.Guid
                        where accounts.Guid == guid
                        select roles.Name;

            return Accountroles;  
        }
    }
}
