namespace RestAPI.Contracts
{
    public interface IGeneralRepository<TEntity>
    {
        TEntity Create(TEntity university);
        bool Update(TEntity university);
        bool Delete(Guid guid);
        IEnumerable<TEntity> GetAll();
        TEntity GetByGuid(Guid guid);

    }
}
