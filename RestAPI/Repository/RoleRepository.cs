using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class RoleRepository : IUniversityRepository<Role>
    {
        private readonly BookingManagementContext _context;

        public RoleRepository(BookingManagementContext context)
        {
            _context = context;
        }

        public Role Create(Role role)
        {
            try
            {
                _context.Set<Role>().Add(role);
                _context.SaveChanges();
                return role;
            }
            catch
            {
                return new Role();
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var role = GetByGuid(guid);
                if (role is null)
                {
                    return false;
                }
                _context.Set<Role>().Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>().ToList();
        }

        public Role GetByGuid(Guid guid)
        {
            return _context.Set<Role>().Find(guid);
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Set<Role>().Update(role);
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
