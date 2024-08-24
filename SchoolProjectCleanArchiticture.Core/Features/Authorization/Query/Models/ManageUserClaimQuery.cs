using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Models
{
    public  class ManageUserClaimQuery:IRequest<ResponseM<ManageUserClaims>>
    {
        public string UserId { get; set; }

        public ManageUserClaimQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
