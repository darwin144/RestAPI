using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        public EducationRepository(BookingManagementContext context) : base(context)
        {
        }
    }
}
