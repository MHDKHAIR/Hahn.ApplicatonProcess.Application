using System.ComponentModel.DataAnnotations.Schema;
using Hahn.ApplicatonProcess.July2021.Data.Common;

namespace Hahn.ApplicatonProcess.July2021.Domain.Entities
{
    public class Asset : BaseEntity<string>
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
