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
        private readonly IMapper<Room, RoomVM> _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
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

        [HttpGet("room")]
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
