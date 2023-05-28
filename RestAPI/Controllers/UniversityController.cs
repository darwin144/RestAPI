using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.AccontRole;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class UniversityController : GeneralController<University, UniversityVM>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper<University, UniversityVM> _mapper;

        public UniversityController(IUniversityRepository universityRepository, IMapper<University, UniversityVM> mapper): base(universityRepository,mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }
    }
}
