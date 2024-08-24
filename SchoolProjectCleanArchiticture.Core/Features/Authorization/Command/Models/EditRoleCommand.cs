using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models
{
    public  class EditRoleCommand:EditRoleRequest,IRequest<ResponseM<string>>
    {
        
    }
}
