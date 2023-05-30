using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoomController : GeneralController<Room, RoomVM>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Room, RoomVM> _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper) : base(roomRepository, mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var respons = new ResponseVM<IEnumerable<RoomUsedVM>>();
            try
            {          
                var room = _roomRepository.GetCurrentlyUsedRooms();
                if (room.Count() < 1)
                {
                    return NotFound(ResponseVM<RoomUsedVM>.NotFound(room));
                }

                return Ok(ResponseVM<IEnumerable<RoomUsedVM>>.Successfully(room));
            }
            catch (Exception ex) {

                return BadRequest(ResponseVM<string>.Error(ex.Message));
            }
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
           
            try
            {
                var room = _roomRepository.GetByDate(dateTime);
                if (room is null)
                {
                    return NotFound(ResponseVM<MasterRoomVM>.NotFound(room));
                }

                return Ok(ResponseVM<IEnumerable<MasterRoomVM>>.Successfully(room));
            }
            catch(Exception ex) {
                return BadRequest(ResponseVM<string>.Error(ex.Message));
            }
        }
        [HttpGet("RoomAvailable")]
        public IActionResult GetRoomByDate()
        {
            try
            {
                var room = _roomRepository.GetRoomByDate();
                if (room is null)
                {
                    return NotFound(ResponseVM<RoomBookedTodayVM>.NotFound(room));
            
                }

                return Ok(ResponseVM<IEnumerable<RoomBookedTodayVM>>.Successfully(room));
            }
            catch(Exception ex)
            {
                return NotFound(ResponseVM<RoomBookedTodayVM>.Error(ex.Message));
            }
        }

        
    }
}
