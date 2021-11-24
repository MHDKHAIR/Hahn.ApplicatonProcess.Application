using Hahn.ApplicatonProcess.July2021.Domain.Entities;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IStoreContext
    {
        User CurrentUser { get; set; }
    }
}
