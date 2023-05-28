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
    public class AccountRoleController : GeneralController<AccountRole, AccountRoleVM>
    {
        private readonly IAccountRoleRepository _AccountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper) : base (accountRoleRepository, mapper)
        {
            _AccountRoleRepository = accountRoleRepository;
            _mapper = mapper;
        }
    }
}
