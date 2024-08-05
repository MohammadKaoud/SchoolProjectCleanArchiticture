using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Query.Models
{
    public class GetUserByNameQuery:IRequest<ResponseM<UserToView>>
    {
        public string UserName { get; set; }
        public GetUserByNameQuery(string userName)
        {
            this.UserName = userName;
        }
    }
}
