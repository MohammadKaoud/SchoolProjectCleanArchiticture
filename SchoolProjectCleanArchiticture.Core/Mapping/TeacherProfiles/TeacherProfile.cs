using SchoolProjectCleanArchiticture.Core.Features.Teacher.Commands.Models;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Mapping
{
    public partial class TeacherProfile
    {
        public void MappingAddTeacherCommand()
        {
            CreateMap<AddTeacherCommand,Teacher>().ReverseMap();
        }
    }
}
