using Data.Entities;

namespace Data
{
    public interface IRepositoryGeneric<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<List<TEntity>> GetAll();
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> GetById(TKey id, IEnumerable<string> property = null);
    }
}
