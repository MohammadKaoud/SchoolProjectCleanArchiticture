using Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<SchoolProjectCleanArchiticture.Core.Base.ResponseM<string>>
    {
        public int Id { get; set; }

    }
}
