using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data
{
    public class JwtSettings
    {
        public string Issuer {  get; set; }
        public string Key { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }  

        public bool ValidateIssuerSigninngkey { get; set; }
        public bool ValidateLifeTime { get; set; }
        public int RefreshTokenExpiredAt { get; set; }
        public int AccessTokenExpiredAt { get; set; }



    }
}
