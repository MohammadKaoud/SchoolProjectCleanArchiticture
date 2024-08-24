using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Views
{
    [Keyless]
    public  class DepartmentView
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int StudentCount { get; set; }

    }
}
