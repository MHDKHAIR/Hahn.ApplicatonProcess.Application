using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.July2021.Domain.Common
{
    public class StoreContext : IStoreContext
    {
        public User CurrentUser { get; set; }
    }
}
