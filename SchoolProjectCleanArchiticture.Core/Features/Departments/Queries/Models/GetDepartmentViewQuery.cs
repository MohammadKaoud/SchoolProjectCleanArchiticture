using Azure;
using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Departments.Queries.Models
{
    public  class GetDepartmentViewQuery:IRequest<ResponseM<List<DepartmentView>>>
    {
    }
}
