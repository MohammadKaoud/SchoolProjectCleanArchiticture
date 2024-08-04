using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public  interface IStudentService
    {
       

        public Task<IEnumerable<Student>>GetStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddStudentAsync(Student student);
        public Task<bool>IsNameExist(string name);
       public Task<string>UpdateAsync(Student student);
        public Task<string> DeleteAsync(int id);
        public IQueryable<Student> GetAllStudentsQueryable();
        public IQueryable<Student> GetAllStudentsQueryableSearch(StudentOrderingEnum orderingEnum,string search);
        public IQueryable<Student> GetQueryableStudentWithRelatedDepartment(int DepartmentId);


    }
}
