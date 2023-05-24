using RestAPI.Model;
using RestAPI.ViewModels.Rooms;

namespace RestAPI.Contracts
{
    public interface IRoomRepository : IGeneralRepository<Room>
    {
        RoomBookedTodayVM GetRoomByGuid(Guid guid);
        IEnumerable<RoomBookedTodayVM> GetRoomByDate();
    }
}
