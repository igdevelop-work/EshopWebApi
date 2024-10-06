using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EshopWebApi.backend.shared.cqrs
{
    public abstract class BaseWriteService<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseWriteService(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        protected async Task _create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        protected async Task _update(TEntity entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        protected async Task _delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }
    }
}