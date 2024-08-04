
using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Queries.Models
{
    public  class GetSingleStudent:IRequest<ResponseM<StudentDto>>
    {
        public int Id { get; set; }

    }
}
