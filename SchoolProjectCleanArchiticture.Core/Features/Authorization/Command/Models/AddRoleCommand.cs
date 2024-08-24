using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models
{
    public  class AddRoleCommand:IRequest<ResponseM<string>>
    {
       public  string RoleName { get; set; }
        public AddRoleCommand(string roleName)
        {
            RoleName = roleName;
        }
    }
}
