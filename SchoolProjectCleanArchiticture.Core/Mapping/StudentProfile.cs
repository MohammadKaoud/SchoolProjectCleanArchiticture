using AutoMapper;
using SchoolProjectCleanArchiticture.Core.Dtos;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Mapping
{
    public partial class StudentProfile : Profile
    {

        public StudentProfile()
        {
            GetStudentDtos();
            AddStudentDto();
            EditStudentDto();

        }

    }
}
