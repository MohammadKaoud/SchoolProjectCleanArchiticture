using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models
{
    public  class DeleteRoleCommand:IRequest<ResponseM<string>>
    {
        public string Id {  get; set; }
        public DeleteRoleCommand(string Id)
        {
            this.Id = Id;
        }
    }
}
