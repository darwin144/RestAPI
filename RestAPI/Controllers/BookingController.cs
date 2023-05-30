using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.Repository;
using RestAPI.Utility;
using RestAPI.ViewModels.Bookings;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Employees;
using RestAPI.ViewModels.Rooms;
using RestAPI.ViewModels.Universities;

namespace RestAPI.Controllers
{
  
    public class BookingController : GeneralController<Booking,BookingVM> 
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _maper;

        public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> mapper) : base(bookingRepository, mapper)
        {
            _bookingRepository = bookingRepository;
            _maper = mapper;
        }

        [HttpGet("bookingduration")]
        public IActionResult GetDuration()
        {
            try
            {
                var bookingLengths = _bookingRepository.GetBookingDuration();
                if (!bookingLengths.Any())
                {
                    return NotFound(new ResponseVM<IEnumerable<BookingDurationVM>>
                    {
                        Code = 400,
                        Status = "Failed",
                        Message = "Data Not Found",
                        Data = bookingLengths
                    }
                  );
                }
                return Ok(new ResponseVM<IEnumerable<BookingDurationVM>>
                {
                    Code = 200,
                    Status = "OK",
                    Message = "Success",
                    Data = bookingLengths
                }
                   );
            }
            catch(Exception ex) {
                return BadRequest(new ResponseVM<BookingDetailVM>
                {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message
                }
                );
            }
        }

        [Authorize(Roles = nameof(RoleLevel.Admin))]
        [HttpGet("AllBookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            try
            {               
                var results = _bookingRepository.GetAllBookingDetail();

                return Ok(new ResponseVM<IEnumerable<BookingDetailVM>>
                {
                    Code = 200,
                    Status = "OK",
                    Message = "Success",
                    Data = results
                 }
                );
            }
            catch {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = 500,
                    Status = "Failed",
                    Message = "Runtime error pada Code",
                    }
                );
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
                    return NotFound(new ResponseVM<BookingDetailVM>
                    {                        
                        Code = 400,
                        Status = "Failed",
                        Message = "Data Not Found",
                        Data = bookingDetailVM
                    }
                    );
                }

                return Ok(new ResponseVM <BookingDetailVM>
                {
                    Code = 200,
                    Status = "OK",
                    Message = "Success",
                    Data = bookingDetailVM
                }
                );
                
            }
            catch(Exception ex) {
                return BadRequest(new ResponseVM<BookingDetailVM>
                    {
                    Code = 500,
                    Status = "Error",
                    Message = ex.Message                   
                   }
                );
            }
        }

    }
}
