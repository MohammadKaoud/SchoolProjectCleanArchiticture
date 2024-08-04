using Microsoft.Identity.Client;
using SchoolProjectCleanArchiticture.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Departments.Dtos
{
    public  class GetDepartmentByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<SubjectResponse> Subjects { get; set; }
        public PaginatedResult<StudentResponse> Students { get; set; }
        public List<TeacherResponse> Teachers { get; set; }


    }
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentResponse(int Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TeacherResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
