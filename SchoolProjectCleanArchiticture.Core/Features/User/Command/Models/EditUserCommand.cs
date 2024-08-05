using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SchoolProjectCleanArchiticture.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Models
{
    public class EditUserCommand:IRequest<ResponseM<string>>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}
