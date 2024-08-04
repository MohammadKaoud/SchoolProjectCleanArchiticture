using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Departments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Departments.Queries.Models
{
    public  class GetDepartmentByIdQuery:IRequest<ResponseM<GetDepartmentByIdDto>>
    {
        public int Id { get; set; }

        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }

        //public GetDepartmentByIdQuery(int Id,int pageNumber,int PageSize)
        //{
        //    this.Id = Id;
        //    this.StudentPageNumber = pageNumber;
        //    this.StudentPageSize = PageSize;

        //}


    }
}
