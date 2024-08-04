using FluentValidation;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Validators
{
    public  class EditStudentCommandValidator:AbstractValidator<EditStudentCommand>
    {

        public EditStudentCommandValidator()
        {
            NormalValidation();
        }
        public void NormalValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Do not Left it Null please")
            .NotNull().WithMessage("it is not nullable ");

        }
    }
}
