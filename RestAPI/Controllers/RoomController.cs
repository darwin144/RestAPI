using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("RestAPI/[controller]")]
    public class RoomController : GeneralController<Room, RoomVM, IRoomRepository>
    {
        private readonly IRoomRepository _roomRepository;
        
        public RoomController(IRoomRepository roomRepository, IGeneralRepository<Room> generalRepository, IMapper<Room, RoomVM> mapper) : base(generalRepository, mapper)
        {
            _roomRepository = roomRepository;
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
                    return NotFound(respons.NotFound(room));
                }
                var succes = respons.Success(room);
                return Ok(succes);
            }
            catch (Exception ex) {

                return BadRequest(respons.Error(ex.Message));
            }
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var respons = new ResponseVM<IEnumerable<MasterRoomVM>>();
            try
            {
                var room = _roomRepository.GetByDate(dateTime);
                if (room is null)
                {
                    return NotFound(respons.NotFound(room));
                }

                return Ok(respons.Success(room));
            }
            catch(Exception ex) {
                return BadRequest(respons.Error(ex.Message));
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
                    return NotFound(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
                    {
                        Code = 400,
                        Status = "Not Found",
                        Message = "Ruangan Tidak Ditemukan",
                        Data = room
                    }
                    );
                }

                return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
                {

                    Code = 200,
                    Status = "OK",
                    Message = "Success",
                    Data = room
                }
                );
            }
            catch
            {
                return NotFound(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = 500,
                    Status = "Failed",
                    Message = "Runtime error acces Server",
                }
              );
            }
        }

        
    }
}
