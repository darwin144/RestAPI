using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;

namespace RestAPI.Repository
{
    public class RoleRepository : BaseRepository<Role>,IRoleRepository
    {
        public RoleRepository(BookingManagementContext context) : base(context)
        {
        }


    }
}
