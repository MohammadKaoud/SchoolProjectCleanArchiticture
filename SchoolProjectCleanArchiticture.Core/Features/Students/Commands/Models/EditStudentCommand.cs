
using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models
{
    public  class EditStudentCommand:IRequest<ResponseM<string>>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }

    }
}
