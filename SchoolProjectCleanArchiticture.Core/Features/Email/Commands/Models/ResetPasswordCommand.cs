using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models
{
    public  class ResetPasswordCommand:IRequest<ResponseM<string>>
    {
        public string Email { get; set; }

    }
}
