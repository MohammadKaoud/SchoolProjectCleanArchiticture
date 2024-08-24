using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Models
{
    public  class GetRoleByIdQuery:IRequest<ResponseM<GetRoleResult>>
    {
        public string Id { get; set; }
        public GetRoleByIdQuery(string Id)
        {
            this.Id = Id;
        }
    }
}
