using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data
{
    public class JwtReponseAuth
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        
    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string RefreshedToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
