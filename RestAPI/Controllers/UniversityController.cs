using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository<University> _universityRepository;

        public UniversityController(IUniversityRepository<University> universityRepository)
        {
            _universityRepository = universityRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any()) {
                return NotFound();
            }

            return Ok(universities);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null) {
                return NotFound();
            }
            return Ok(university);
        }
        [HttpPost]
        public IActionResult Create(University university) {
            
            var result = _universityRepository.Create(university);
            if (result is null) {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(University university) {
            var isUpdated = _universityRepository.Update(university);
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
