using Microsoft.EntityFrameworkCore;
namespace Data.EntityFrameworkCore
{
    public class RepositoryGeneric<TEntity>
        where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _entitySet;

        protected RepositoryGeneric(DbContext context)
        {
            _context = context;
            _entitySet = context.Set<TEntity>();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _entitySet.AddAsync(entity);
            await Save();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _entitySet.ToListAsync();
        }
    }
}
