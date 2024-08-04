using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public interface IDepartmentRepo:IGernericRepos<Department>
    {
        public Task<Department> GetDepartmentByIdAsync(int id);

    }
}
