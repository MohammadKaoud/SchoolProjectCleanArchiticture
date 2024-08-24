using FluentValidation;
using SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Validators
{
    public  class ResetPasswordValidator:AbstractValidator<ResetPasswordOperation>
    {
        public ResetPasswordValidator()
        {
            ResetPasswordValidation();
        }
        public void ResetPasswordValidation()
        {
            RuleFor(x => x.Password).Matches(x => x.PasswordConfirmation).WithMessage("Should Be The Same");
        }
    }
}
