using Azure;
using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Query.Models
{
    public class GetPaginatedQuery :IRequest<PaginatedResult<GetPaginatedUsers>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
