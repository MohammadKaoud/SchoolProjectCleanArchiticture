using SchoolProjectCleanArchiticture.Core.Dtos;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Mapping
{
    public partial class StudentProfile
    {
        public void  GetStudentDtos()
        {
            CreateMap<Student, StudentDto>()
                          .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.LocalizedName ))
                          .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName)).ReverseMap();

            
           
           
        }


    }
    
}
