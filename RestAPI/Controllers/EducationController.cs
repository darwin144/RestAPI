using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducatiionRepository _educationRepository;
        private readonly IMapper<Education, EducationVM> _mapper;

        public EducationController(IEducatiionRepository educationRepository, IMapper<Education, EducationVM> mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound();
            }
            var educationConverted = educations.Select(_mapper.Map).ToList();

            return Ok(educationConverted);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var education = _educationRepository.GetByGuid(guid);
            if (education is null)
            {
                return NotFound();
            }
            //var educationConverted = EducationVM.ToVM(education);

            //menggunakan mapper
            var educationConverted = _mapper.Map(education);
            return Ok(educationConverted);
        }
        [HttpPost]
        public IActionResult Create(EducationVM educationVM)
        {
            //var EducationConverted = EducationVM.ToModel(educationVM);
            var educationConverted = _mapper.Map(educationVM);
            var result = _educationRepository.Create(educationConverted);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(EducationVM educationVM)
        {
            var educationConverted = EducationVM.ToModel(educationVM);
            var isUpdated = _educationRepository.Update(educationConverted);

            if (!isUpdated)
            {
                return BadRequest();
            }
            
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _educationRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
