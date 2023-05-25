using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper<University, UniversityVM> _mapper;
        private readonly IMapper<Education, EducationVM> _educationMapper;

        public UniversityController(IUniversityRepository universityRepository, IEducationRepository educationRepository, IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationMapper = educationMapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            
            var universities = _universityRepository.GetAll();
            if (!universities.Any()) {
                return NotFound();
            }

            // data transfer object
            var resultConverted = universities.Select(_mapper.Map).ToList();
            return Ok(resultConverted);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) {

            
            var university = _universityRepository.GetByGuid(guid);
            if (university is null) {
                return NotFound();
            }
            //var universityConverted = UniversityVM.ToVM(university);
            var universityConverted = _mapper.Map(university);
            return Ok(universityConverted);
        }
        [HttpPost]
        public IActionResult Create(UniversityVM universityVm) {

            //var universityConverted = UniversityVM.ToModel(universityVm);

            var universityConverted = _mapper.Map(universityVm);

            var result = _universityRepository.Create(universityConverted);
            if (result is null) {
                return BadRequest();
            }
            
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(UniversityVM universityVM) {

            //var UniversityConverted = UniversityVM.ToModel(universityVM);
            var universityConverted = _mapper.Map(universityVM);

            var isUpdated = _universityRepository.Update(universityConverted);
            if (!isUpdated) {
                return BadRequest();
            }
            return Ok();        
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid) {
            var isDeleted = _universityRepository.Delete(guid);
            if (!isDeleted) {
                return BadRequest();
            }
            return Ok();
        }

    }
}
