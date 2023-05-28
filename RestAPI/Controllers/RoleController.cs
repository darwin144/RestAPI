using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Roles;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoleController : GeneralController<Role, RoleVM>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper<Role, RoleVM> _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper) : base(roleRepository, mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
    }
}
