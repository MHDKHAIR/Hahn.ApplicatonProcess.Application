using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hahn.ApplicatonProcess.July2021.Data.Common;

namespace Hahn.ApplicatonProcess.July2021.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public User()
        {
            Assets = new HashSet<Asset>();
        }
        [Required]
        public int Age { get; set; }
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
