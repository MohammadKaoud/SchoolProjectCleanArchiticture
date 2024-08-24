using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public  interface IDepartmentService
    {
        public Task<Department>GetDepartmentByIdAsync(int id);
        public Task<bool> IsDepartmentExist(int id);
        public Task<List<DepartmentView>> GetDepartmentView();
    }

}
