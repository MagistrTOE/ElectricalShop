namespace Data
{
    public interface IRepositoryGeneric<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task Add(TEntity entity);
    }
}
