using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Core.Exceptions;

namespace Data.EntityFrameworkCore
{
    public class RepositoryGeneric<TEntity, TKey> : IRepositoryGeneric<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
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

        public async Task<TEntity> GetById(TKey id, IEnumerable<string> property = null)
        {

             var entity = _entitySet
                .AsQueryable()
                .Where(x => x.Id.Equals(id));

             if (property?.Any() ?? false)
                 entity = property.Aggregate(entity, (currentProperty, nextProperty) => currentProperty.Include(nextProperty));

             return await entity.SingleOrDefaultAsync();
        }

        public Task Delete(TEntity entity)
        {
            _entitySet.Remove(entity);

            return Save();
        }
    }
}
