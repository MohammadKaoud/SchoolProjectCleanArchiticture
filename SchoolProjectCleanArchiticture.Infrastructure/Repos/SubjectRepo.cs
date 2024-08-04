using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Repos
{
    public  class SubjectRepo:GenericRepo<Subject>, ISubjectRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Student> _students;
        public SubjectRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;

            _students = _context.students;
        }
       
    }
}
