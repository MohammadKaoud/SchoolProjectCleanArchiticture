using MediatR;
using SchoolProjectCleanArchiticture.Core.Dtos;
using SchoolProjectCleanArchiticture.Core.Wrapper;
using SchoolProjectCleanArchiticture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Queries.Models
{
    public  class GetPaginatedResult:IRequest<PaginatedResult<StudentDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string?Search { get; set; }

    }
}


