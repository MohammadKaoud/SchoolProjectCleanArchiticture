using MediatR;
using Microsoft.AspNetCore.ResponseCaching;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Query.Models
{
    public  class DeleteUser:IRequest<ResponseM<string>>
    {
        public string Id    { get; set; }
        public DeleteUser(string id)
        {
            Id= id; 
        }

    }
}
