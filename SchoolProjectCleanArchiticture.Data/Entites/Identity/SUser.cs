using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Entites.Identity
{
    public  class SUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

    }
}
