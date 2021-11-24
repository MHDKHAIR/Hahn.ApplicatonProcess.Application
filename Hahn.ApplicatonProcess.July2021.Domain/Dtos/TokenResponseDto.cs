using System;

namespace Hahn.ApplicatonProcess.July2021.Domain.Dtos
{
    public class TokenResponseDto : BaseDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
