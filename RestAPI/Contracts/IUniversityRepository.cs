using RestAPI.Model;

namespace RestAPI.Contracts
{
    public interface IUniversityRepository<Tobject>
    {
        Tobject Create(Tobject university);
        bool Update(Tobject university);
        bool Delete(Guid guid);
        IEnumerable<Tobject> GetAll();
        Tobject GetByGuid(Guid guid);
    }
}
