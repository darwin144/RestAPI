using RestAPI.Model;

namespace RestAPI.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University>
    {
        University CreateWithValidate(University university);
    }
}
