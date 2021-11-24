using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFrameworkPaginateCore;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, bool AsNoTracking = true, params string[] includes);
        Task<TEntity> FirstByConditionAsync(Expression<Func<TEntity, bool>> expression, bool AsNoTracking = true, params string[] includes);
        Task<TEntity> FirstOrDefaultAsync(bool AsNoTracking = true, params string[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking = true, params string[] includes);
        Task<TEntity> GetAsync(object id, bool AsNoTracking = true, params string[] includes);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> LastByConditionAsync(Expression<Func<TEntity, bool>> expression, bool AsNoTracking = true, params string[] includes);
        Task<TEntity> LastOrDefaultAsync(bool AsNoTracking = true, params string[] includes);
        Task<Page<TEntity>> PaginateAsync(int page, int count, Sorts<TEntity> sorts, Filters<TEntity> filters, bool AsNoTracking = true, params string[] includes);
        Task RemoveAsync(object id, bool DeleteSoftDeleted = true);
        Task<TEntity> SoftDeleteAsync(object id);
    }
}
