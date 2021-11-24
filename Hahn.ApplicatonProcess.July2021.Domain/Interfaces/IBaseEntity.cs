using System;
using Hahn.ApplicatonProcess.July2021.Domain.Common;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IBaseEntity
    {
        Status Status { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }

}
