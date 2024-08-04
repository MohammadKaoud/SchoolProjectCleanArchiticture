using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Mapping
{
    public partial class StudentProfile
    {
       public void AddStudentDto()
        {


            CreateMap<AddStudentCommand, Student>()
      .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
      .ForMember(dest => dest.LocalizedName, opt => opt.MapFrom(src => src.Name))
   
      .ReverseMap();
        }
        public void EditStudentDto()
        {
            CreateMap<EditStudentCommand, Student>().ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))

                .ForMember(dest => dest.LocalizedName, opt => opt.MapFrom(src => src.Name));
        }


    }
}
