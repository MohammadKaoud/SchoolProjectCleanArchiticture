using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models
{
    public class ResetPasswordOperation : IRequest<ResponseM<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

    }


}
