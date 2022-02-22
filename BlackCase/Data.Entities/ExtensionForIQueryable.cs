using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public static class ExtensionForIQueryable
    {
        public static IQueryable<TEntity> IncludeEntities<TEntity>(this IQueryable<TEntity> entities, IEnumerable<string> properties)
            where TEntity : class
        {
            if (properties == null)
            {
                return entities;
            }
            return properties
                .Aggregate(entities, (currentProperty, nextProperty) => currentProperty.Include(nextProperty));
        }
    }
}
