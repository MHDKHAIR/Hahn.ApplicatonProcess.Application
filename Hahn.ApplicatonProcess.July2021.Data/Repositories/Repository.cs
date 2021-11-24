using EntityFrameworkPaginateCore;
using Hahn.ApplicatonProcess.July2021.Domain.Common;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected ApplicationDbContext _context;
        private DbSet<TEntity> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        #region Get
        public async Task<TEntity> GetAsync(object id, bool AsNoTracking = true, params string[] includes)
        {
            TEntity include = default;
            var orginal = await dbSet.FindAsync(id);
            if (orginal == null)
                return null;
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();

            if (includes == null)
            {
                return orginal;
            }
            else
            {
                foreach (var item in includes)
                {
                    if (item != null)
                        set = set.Include(item);
                }
                include = await set.FirstOrDefaultAsync(x => x == orginal);
                return include;
            }
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression)
        {
            var set = dbSet.AsQueryable();
            return await set.CountAsync(expression);
        }

        #endregion

        #region GetAll
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }
            return await set.ToListAsync();
        }
        #endregion

        #region Find
        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression, bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }

            var data = set
                    .Where(expression);
            return await data.ToListAsync();
        }
        #endregion

        #region First
        public async Task<TEntity> FirstByConditionAsync(Expression<Func<TEntity, bool>> expression, bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }

            return await set.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> FirstOrDefaultAsync(bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }

            return await set.FirstOrDefaultAsync();
        }

        #endregion

        #region LastOrDefault
        public async Task<TEntity> LastByConditionAsync(Expression<Func<TEntity, bool>> expression, bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }
            return await set.LastOrDefaultAsync(expression);
        }

        public async Task<TEntity> LastOrDefaultAsync(bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }
            return await set.LastOrDefaultAsync();
        }

        #endregion

        #region Insert

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new GenericSystemException(ErrorCodes.NotFound, string.Concat(entity.GetType().Name, nameof(ErrorCodes.NotFound)));
            }
            await dbSet.AddAsync(entity);
            return entity;
        }

        #endregion

        #region DeleteOrRemove
        public async Task RemoveAsync(object id, bool DeleteSoftDeleted = true)
        {
            TEntity entity = await dbSet.FindAsync(id);
            if (entity != null && entity.Status == Status.Deleted && !DeleteSoftDeleted)
            {
                throw new GenericSystemException(ErrorCodes.NotFound, string.Concat(entity.GetType().Name, nameof(ErrorCodes.NotFound)));
            }
            dbSet.Remove(entity);
        }
        public async Task<TEntity> SoftDeleteAsync(object id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new GenericSystemException(ErrorCodes.NotFound, string.Concat(entity.GetType().Name, nameof(ErrorCodes.NotFound)));
            }
            entity.Status = Status.Deleted;
            return entity;
        }
        #endregion

        #region PaginateAsync
        public async Task<Page<TEntity>> PaginateAsync(int page, int count, Sorts<TEntity> sorts, Filters<TEntity> filters, bool AsNoTracking = true, params string[] includes)
        {
            var set = dbSet.AsQueryable();
            if (AsNoTracking)
                set = dbSet.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                        set = set.Include(include);
                }
            }
            var result = await set.PaginateAsync(page, count < 0 ? int.MaxValue : count, sorts, filters);
            return result;

        }
        #endregion
    }
}
