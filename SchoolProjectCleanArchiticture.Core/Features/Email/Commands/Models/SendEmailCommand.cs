using MediatR;
using Microsoft.Extensions.Primitives;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models
{
    public  class SendEmailCommand:IRequest<ResponseM<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }

    }
}
