using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.ViewModels.Educations;
using RestAPI.ViewModels.Employees;
using RestAPI.ViewModels.Rooms;
using RestAPI.ViewModels.Universities;
using System.Globalization;

namespace RestAPI.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly IBookingRepository _contextBooking;
        public RoomRepository(BookingManagementContext context, IBookingRepository booking) : base(context)
        {
            _contextBooking = booking;
        }

        public IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime)
        {
            var rooms = GetAll();
            var bookings = _context.Bookings.ToList();
            var employees = _context.Employees.ToList();

            var usedRooms = new List<MasterRoomVM>();

            foreach (var room in rooms)
            {
                var booking = bookings.FirstOrDefault(b => b.RoomGuid == room?.Guid && b.StartDate <= dateTime && b.EndDate >= dateTime);
                if (booking != null)
                {
                    var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                    var result = new MasterRoomVM
                    {
                        BookedBy = employee.FirstName + " " + employee?.LastName,
                        Status = booking.Status.ToString(),
                        RoomName = room.Name,
                        Floor = room.Floor,
                        Capacity = room.Capacity,
                        StartDate = booking.StartDate,
                        EndDate = booking.EndDate,
                    };

                    usedRooms.Add(result);
                }
            }

            return usedRooms;
        }

        public IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms()
        {
            var rooms = GetAll();
            var bookings = _context.Bookings.ToList();
            var employees = _context.Employees.ToList();

            var usedRooms = new List<RoomUsedVM>();

            var currentTime = DateTime.Now;

            foreach (var room in rooms)
            {
                var booking = bookings.FirstOrDefault(b => b.RoomGuid == room?.Guid && b.StartDate <= currentTime && b.EndDate >= currentTime);
                if (booking != null)
                {
                    var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                    var result = new RoomUsedVM
                    {
                        RoomName = room.Name,
                        Status = booking.Status.ToString(),
                        Floor = room.Floor,
                        BookedBy = employee.FirstName + " " + employee.LastName,
                    };

                    usedRooms.Add(result);
                }
            }
            return usedRooms;
        }
        public IEnumerable<RoomBookedTodayVM> GetRoomByDate() {
            try
            {
                //get all data from booking and rooms
                var booking = _contextBooking.GetAll();
                var rooms = GetAll();
                    
                var startToday = DateTime.Today;
                var endToday = DateTime.Today.AddHours(23).AddMinutes(59);

                var roomUse = rooms.Join(booking, Room => Room.Guid, booking => booking.RoomGuid,( Room, booking) => 
                new { Room, booking})
                        .Select(joinResult => new {
                            joinResult.Room.Name,
                            joinResult.Room.Floor,
                            joinResult.Room.Capacity,
                            joinResult.booking.StartDate,
                            joinResult.booking.EndDate
                        }
                 );
                var roomUseTodays = new List<RoomBookedTodayVM>();                                
                foreach (var room in roomUse)
                {
                    if ((room.StartDate > startToday && room.EndDate > endToday) || (room.StartDate < startToday && room.EndDate < startToday))
                    {
                        var roomDay = new RoomBookedTodayVM
                        {
                            RoomName = room.Name,
                            Floor = room.Floor,
                            Capacity = room.Capacity,
                        };
                        roomUseTodays.Add(roomDay);
                    }
                };
                return roomUseTodays;
            }
            catch
            {
                return null;

            }
        }
    }
}
