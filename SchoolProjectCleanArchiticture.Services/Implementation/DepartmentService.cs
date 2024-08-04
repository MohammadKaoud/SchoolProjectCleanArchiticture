
using Microsoft.EntityFrameworkCore;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Infrastructure.Repos;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _repo;
        public DepartmentService(IDepartmentRepo repo)
        {
            _repo = repo;
        }
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
         
                
            var result=await _repo.GetDepartmentByIdAsync(id);
            if (result == null)
            {
                return null;
            }
            return result;  


        }

        public async  Task<bool> IsDepartmentExist(int id)
        {
         var result= await _repo.GetTableAsNoTracking().AnyAsync(x=>x.DepartmentId == id);
            if (!result)
            {
                return false;
            }
            return true;
        }
    }
}
