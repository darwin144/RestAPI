using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class UniversityController : GeneralController<University, UniversityVM, IUniversityRepository>
    {
        public UniversityController(IGeneralRepository<University> generalRepository, IMapper<University, UniversityVM> mapper) : base(generalRepository, mapper)
        {
        }

    }
}
