using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.ViewModels.Bookings;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Employees;
using RestAPI.ViewModels.Rooms;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{

    [ApiController]
    [Route("RestAPI/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bokingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingController(IBookingRepository bokingRepository, IMapper<Booking, BookingVM> mapper, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bokingRepository = bokingRepository;
            _mapper = mapper;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                
                var results = _bokingRepository.GetAllBookingDetail();

                return Ok(results);
            }
            catch {
                return Ok("Ada Error");
            }

        }
        [HttpGet("BookingDetail/{guid}")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            try
            {
                var bookingDetailVM = _bokingRepository.GetBookingDetailByGuid(guid);

                if (bookingDetailVM is null)
                {
                    return Ok("Tidak ditemukan objek dengan Guid ini");
                }
                

                return Ok(bookingDetailVM);
            }
            catch {
                return Ok("Ada Error");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bokingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound();
            }
            var bookingConverteds = bookings.Select(_mapper.Map).ToList();
            return Ok(bookingConverteds);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bokingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound();
            }
            var bookingConverted = _mapper.Map(booking);
            return Ok(bookingConverted);
        }
        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var booking = _mapper.Map(bookingVM);
            var result = _bokingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var booking = _mapper.Map(bookingVM);
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
