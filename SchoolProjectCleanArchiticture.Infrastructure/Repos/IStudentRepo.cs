using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public interface IStudentRepo:IGernericRepos<Student>
    {
        public Task<IEnumerable<Student>> GetStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id);

    }
}
