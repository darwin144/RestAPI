using RestAPI.Model;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Contracts
{
    public interface IRoomRepository : IGeneralRepository<Room>
    {       
        IEnumerable<RoomBookedTodayVM> GetRoomByDate();
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();
    }
}
