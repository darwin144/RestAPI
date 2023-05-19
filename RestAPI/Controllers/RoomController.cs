using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IUniversityRepository<Room> _universityRepository;

        public RoomController(IUniversityRepository<Room> universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _universityRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }

            return Ok(rooms);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var room = _universityRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }
        [HttpPost]
        public IActionResult Create(Room room)
        {

            var result = _universityRepository.Create(room);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(Room room)
        {
            var isUpdated = _universityRepository.Update(room);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _universityRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
