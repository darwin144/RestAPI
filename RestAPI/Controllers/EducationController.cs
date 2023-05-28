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
    public class EducationController : GeneralController<Education, EducationVM>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _mapper;

        public EducationController(IEducationRepository educationRepository, IMapper<Education, EducationVM> mapper) : base(educationRepository, mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }
    }
}
