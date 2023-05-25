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
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingController(IBookingRepository bokingRepository, IMapper<Booking, BookingVM> mapper, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bokingRepository;
            _mapper = mapper;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }



        [HttpGet("bookingduration")]
        public IActionResult GetDuration()
        {
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound();
            }

            return Ok(bookingLengths);
        }



        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {
                
                var results = _bookingRepository.GetAllBookingDetail();

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
                var bookingDetailVM = _bookingRepository.GetBookingDetailByGuid(guid);

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
            var bookings = _bookingRepository.GetAll();
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
            var booking = _bookingRepository.GetByGuid(guid);
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
            var result = _bookingRepository.Create(booking);
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
            var isUpdated = _bookingRepository.Update(booking);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
