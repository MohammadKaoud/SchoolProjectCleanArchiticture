using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public class StudentRepo : GenericRepo<Student>, IStudentRepo 
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Student> _students;
        public StudentRepo(ApplicationDbContext context):base(context)
        {
            _context = context;

            _students=_context.students;
        }
        public async  Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students =  await _students.Include(x=>x.Department).ToListAsync();
            return students;
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student=_students.AsNoTracking().Include(x=>x.Department).SingleOrDefault(x=>x.Id==id);
            return student;

        }

     
    }
}
