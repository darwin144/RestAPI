using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class RoomRepository : IUniversityRepository<Room>
    {
        private readonly BookingManagementContext _context;

        public RoomRepository(BookingManagementContext context)
        {
            _context = context;
        }

        public Room Create(Room room)
        {
            try
            {
                _context.Set<Room>().Add(room);
                _context.SaveChanges();
                return room;
            }
            catch
            {
                return new Room();
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var room = GetByGuid(guid);
                if (room is null)
                {
                    return false;
                }
                _context.Set<Room>().Remove(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();
        }

        public Room GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid);
        }

        public bool Update(Room room)
        {
            try
            {
                _context.Set<Room>().Update(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
