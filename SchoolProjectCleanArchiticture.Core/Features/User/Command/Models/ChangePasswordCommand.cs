using MediatR;
using Microsoft.AspNetCore.ResponseCaching;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Models
{
    public  class ChangePasswordCommand:IRequest<ResponseM<string>>
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }

       
    }
}
