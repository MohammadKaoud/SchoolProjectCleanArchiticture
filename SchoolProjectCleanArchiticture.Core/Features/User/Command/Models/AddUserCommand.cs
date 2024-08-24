using MediatR;
using Microsoft.Identity.Client;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Models
{
    public  class AddUserCommand:IRequest<ResponseM<string>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public string ?Address { get; set; }
        public string? Country { get; set; }
        public string PhoneNumber { get; set; }
        public string? Role { get; set; }

        

    }
}
