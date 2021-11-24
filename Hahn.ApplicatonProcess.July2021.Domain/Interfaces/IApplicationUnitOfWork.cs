using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IApplicationUnitOfWork
    {
        IRepository<TEntity> GenericRepository<TEntity>() where TEntity : class, IBaseEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
