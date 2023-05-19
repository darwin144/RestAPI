using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class UniversityRepository : IUniversityRepository<University>
    {
        private readonly BookingManagementContext _context;
        public UniversityRepository(BookingManagementContext context) {
            _context = context;
        }

        public University Create(University university) {
            try
            {
                _context.Set<University>().Add(university);
                _context.SaveChanges();
                return university;
            }
            catch {
                return new University();
            }
        }     
        public bool Update(University university)
        {
            try
            {
                _context.Set<University>().Update(university);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;            
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var university = GetByGuid(guid);
                if (university is null)
                {
                    return false;
                }
                 _context.Set<University>().Remove(university);
                 _context.SaveChanges();
                 return true;
            }
            catch{
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();
        }

        public University? GetByGuid(Guid guid)
        {

            return _context.Set<University>().Find(guid);
        }

    }
}
