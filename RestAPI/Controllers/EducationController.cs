using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Employees;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class EducationController : GeneralController<Education, EducationVM, IEducationRepository>
    {
        public EducationController(IGeneralRepository<Education> generalRepository, IMapper<Education, EducationVM> mapper) : base(generalRepository, mapper)
        {
           
        }

    }
}
