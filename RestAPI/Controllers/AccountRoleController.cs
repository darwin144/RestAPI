using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.ViewModels.AccontRole;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class AccountRoleController : GeneralController<AccountRole, AccountRoleVM, IAccountRepository>
    {
        
        public AccountRoleController(IGeneralRepository<AccountRole> generalRepository, IMapper<AccountRole, AccountRoleVM> mapper) : base(generalRepository, mapper)
        {
        }

    }
}
