using RestAPI.Model;
using RestAPI.ViewModels.Accounts;
using RestAPI.ViewModels.Register;

namespace RestAPI.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        int Register(RegisterVM registerVM);
        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);
        LoginVM Login(LoginVM loginVM);

        int UpdateOTP(Guid? employeeId);

        IEnumerable<string>? GetRoles(Guid guid);

    }
}
