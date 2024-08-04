using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProjectCleanArchiticture.Data;
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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _studentRepo;
        private readonly ILocalizationService _localizationService;
        public StudentService(IStudentRepo studentRepo,ILocalizationService localizationService)
        {
            _studentRepo=studentRepo;
            _localizationService=localizationService;
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var students = await  _studentRepo.GetTableAsNoTracking().ToListAsync();

            var studentNameToCheckForIt = _localizationService.GetLocalizedName(student);

            var existStudent =  students.FirstOrDefault(x =>
                _localizationService.AreNamesEqual(x, student));

           

            if (existStudent != null)
            {
                return "exist";
            }

         var result=   await _studentRepo.AddAsync(student);    
            if (result is not null)
            {
                return "Complete";
            }
            return "InternalServerError";
        }



        public async Task<string> DeleteAsync(int id)
        {
            var specificStudent = _studentRepo.GetTableAsNoTracking().FirstOrDefault(x => x.Id == id);
            if (specificStudent != null)
            {
                await _studentRepo.DeleteAsync(specificStudent);
                return "Deleted";
            }
            return "NotDeleted";
        }

        public IQueryable<Student> GetAllStudentsQueryable()
        {
            var waitToConvert = _studentRepo.GetTableIQueryable().Include(x => x.Department);
            return waitToConvert;

        }

        public  IQueryable<Student> GetAllStudentsQueryableSearch(StudentOrderingEnum studentOrdering, string search)
        {
            var students =  _studentRepo.GetStudentsAsync();

            IEnumerable<Student>result=null;
            if (!search.IsNullOrEmpty())
            {
                result=students.Result.Where(x=>x.LocalizedName.Contains(search)||x.Address.Contains(search));

            }
            if (studentOrdering!=null)
            {
                switch (studentOrdering)
                {
                    case StudentOrderingEnum.Id:
                        result = result.OrderBy(x => x.Id); break;
                    case StudentOrderingEnum.Name:
                        result = result.OrderBy(x => x.LocalizedName); break;
                    case StudentOrderingEnum.DepartmentName:
                        result = result.OrderBy(x => x.Department.DepartmentName); break;
                    case StudentOrderingEnum.Address:
                        result = result.OrderBy(x => x.Address); break;


                }
            }

            return  result.AsQueryable();
        }

        public IQueryable<Student> GetQueryableStudentWithRelatedDepartment(int DepartmentId)
        {
         var studentQuery=_studentRepo.GetTableAsNoTracking().Where(x=>x.DepartmentId.Equals(DepartmentId)).AsQueryable();
            return studentQuery;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
         
            var specificStudent=await _studentRepo.GetStudentByIdAsync(id);
            return specificStudent;

           
        }

        public  async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = await _studentRepo.GetStudentsAsync();
            return students;
        }

        public async Task<bool> IsNameExist(string name)
        {
            var students = await _studentRepo.GetTableAsNoTracking().ToListAsync();
            var existStudent = students.FirstOrDefault(x=>x.LocalizedName==name); 
            if (existStudent != null)
            {
                return true;

            }
            return false;
        }
        public async Task<string>UpdateAsync(Student student)
        {
            var students =await  _studentRepo.GetTableAsNoTracking().ToListAsync();
            var CheckThestudentExistince=students.FirstOrDefault(x=>x.Id==student.Id);  
            if (CheckThestudentExistince != null)
            {
                await  _studentRepo.UpdateAsync(student);
                return "c";
            }


            return "notComplete";
        }
    }
}
