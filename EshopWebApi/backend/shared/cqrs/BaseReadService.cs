using Microsoft.EntityFrameworkCore;


namespace EshopWebApi.backend.shared.cqrs
{
    public abstract class BaseReadService<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        protected BaseReadService(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        protected async Task<TEntity> _findOne(Func<TEntity, bool> predicate, string errorMessage)
        {
            var entity = _dbSet.FirstOrDefault(predicate);
            if (entity == null)
            {
                throw new Exception(errorMessage);
            }

            return await Task.FromResult(entity);
        }

        protected async Task<IEnumerable<TEntity>> _findAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}