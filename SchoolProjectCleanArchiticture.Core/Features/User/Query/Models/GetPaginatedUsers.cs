﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Query.Models
{
    public  class GetPaginatedUsers
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ? Country { get; set; }
        public string? Address { get; set; }

    }
}
