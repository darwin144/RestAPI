using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Room, RoomVM> _mapper;

        public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, IMapper<Room, RoomVM> mapper)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound();
            }

            return Ok(room);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound();
            }
            var roomConverted = rooms.Select(_mapper.Map).ToList();

            return Ok(roomConverted);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound();
            }
            var roomConverted = _mapper.Map(room);
            return Ok(roomConverted);
        }

        [HttpGet("RoomAvailable")]
        public IActionResult GetRoomByDate()
        {
            try
            {
                var room = _roomRepository.GetRoomByDate();
                if (room is null)
                {
                    return Ok("tidak ada data");
                }

                return Ok(room);
            }
            catch {
                return Ok("ada error");
            }
        }

        [HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {

            var room = _mapper.Map(roomVM); 
            var result = _roomRepository.Create(room);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);

        }
        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var room = _mapper.Map(roomVM);
            var isUpdated = _roomRepository.Update(room);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
