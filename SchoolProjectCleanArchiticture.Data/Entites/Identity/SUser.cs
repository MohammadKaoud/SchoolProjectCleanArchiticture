using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Entites.Identity
{
    public  class SUser:IdentityUser
    {
        //Updated upstream:SchoolProjectCleanArchiticture.Data/Entites/Identity/User.cs

        public SUser()
        {
            RefreshTokens=new HashSet<UserRefreshToken>();    
        }
        public string FullName { get; set; }
 //Stashed changes:SchoolProjectCleanArchiticture.Data/Entites/Identity/SUser.cs
        public string Address { get; set; }
        public string Country { get; set; }

        [InverseProperty(nameof(UserRefreshToken.SUser))]
        public ICollection<UserRefreshToken> RefreshTokens { get; set; }

    }
}
