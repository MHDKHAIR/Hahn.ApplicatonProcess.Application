using System;
using System.ComponentModel.DataAnnotations;
using Hahn.ApplicatonProcess.July2021.Domain.Common;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.July2021.Data.Common
{
    public class BaseEntity<TKey> : IBaseEntity
    {
        public TKey Id { get; set; }
        public Status Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
