using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Teacher.Commands.Models
{
    public  class AddTeacherCommand:IRequest<ResponseM<string>>
    {
        
        public string Name { get; set; }

        public int Position { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }

        public IFormFile? file { get; set; }

        

        public int DepartmentId { get; set; }
    }
}
