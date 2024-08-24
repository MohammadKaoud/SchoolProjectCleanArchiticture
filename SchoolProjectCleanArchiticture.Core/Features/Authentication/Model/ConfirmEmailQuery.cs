using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authentication.Model
{
    public  class ConfirmEmailQuery:IRequest<ResponseM<string>>
    {
        public string UserId {  get; set; }
        public string Code { get; set;   }
    }
}