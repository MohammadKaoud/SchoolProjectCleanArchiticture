﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data
{
    public  static  class ClaimStore
    {
        public static List<Claim> ClaimsData = new ()
        {
          new Claim("Create Student","false"),
          new Claim("Edit Student","false"),
          new Claim("Update Student","false")
        };
    }
}
