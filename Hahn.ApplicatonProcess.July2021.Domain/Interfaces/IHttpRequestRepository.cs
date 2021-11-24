using System.Threading.Tasks;
using System.Threading;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IHttpRequestRepository
    {
        Task<T> GetAsync<T>(string url, CancellationToken cancellationToken);
    }
}
