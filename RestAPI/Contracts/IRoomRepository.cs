using RestAPI.Model;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Contracts
{
    public interface IRoomRepository : IGeneralRepository<Room>
    {       
        IEnumerable<RoomBookedTodayVM> GetRoomByDate();
    }
}
