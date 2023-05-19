using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;

namespace RestAPI.Controllers
{

    [ApiController]
    [Route("RestAPI/[controller]")]
    public class BookingController : Controller
    {
        private readonly IUniversityRepository<Booking> _bokingRepository;

        public BookingController(IUniversityRepository<Booking> bokingRepository)
        {
            _bokingRepository = bokingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bokingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound();
            }

            return Ok(bookings);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bokingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        [HttpPost]
        public IActionResult Create(Booking booking)
        {

            var result = _bokingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(Booking booking)
        {
            var isUpdated = _bokingRepository.Update(booking);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bokingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
