using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Models
{
    public  class ManageUserRolesQuery:IRequest<ResponseM<ManageUserRolesResult>>
    {
        public string UserId { get; set; }
        public ManageUserRolesQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
