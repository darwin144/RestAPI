using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Roles;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoleController : GeneralController<Role, RoleVM, IRoleRepository>
    {
        
        public RoleController(IGeneralRepository<Role> generalRepository, IMapper<Role, RoleVM> mapper) : base(generalRepository, mapper)
        {
        }

       
    }
}
