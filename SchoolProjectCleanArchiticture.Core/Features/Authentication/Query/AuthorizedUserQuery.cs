using Azure;
using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authentication.Query
{
    public  class AuthorizedUserQuery:IRequest<ResponseM<string>>
    {
        public string AccessToken { get; set; }

    }
}
