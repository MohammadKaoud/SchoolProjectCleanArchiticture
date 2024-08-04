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
    public class DepartmentRepo:GenericRepo<Department>,IDepartmentRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Department> departments;
        public DepartmentRepo(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _context = applicationDbContext; 
            departments = _context.departments;
        }

        public async  Task<Department> GetDepartmentByIdAsync(int id)
        {
         var result= await departments.AsNoTracking()
                .Include(x=>x.Manager)
                //.Include(x=>x.Students)
                .Include(x=>x.Subjects)
                .FirstOrDefaultAsync(x=>x.DepartmentId==id);
                return result;
                
        }
    }
}
