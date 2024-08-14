using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Entites.Identity
{
    public  class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(SUser.Id))]
        public string UserId { get; set; }
        [InverseProperty(nameof(SUser.RefreshTokens))]
        public virtual SUser SUser { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public string? JwtId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        
    }
}
