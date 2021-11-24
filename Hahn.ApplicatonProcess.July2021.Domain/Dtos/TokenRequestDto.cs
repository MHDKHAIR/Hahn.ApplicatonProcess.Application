using System.ComponentModel.DataAnnotations;

namespace Hahn.ApplicatonProcess.July2021.Domain.Dtos
{
    public class TokenRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
