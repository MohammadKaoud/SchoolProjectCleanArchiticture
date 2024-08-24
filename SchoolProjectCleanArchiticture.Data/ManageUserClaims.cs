﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data
{
    public class ManageUserClaims
    {
        public string UserId { get; set; }
        public List<UserClaim> UserClaims { get; set; }

    }
    public class UserClaim
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    } 
}
