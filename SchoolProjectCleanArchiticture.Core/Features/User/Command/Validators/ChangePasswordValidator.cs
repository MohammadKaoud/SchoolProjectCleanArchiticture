using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Validators
{
    public  class ChangePasswordValidator:AbstractValidator<ChangePasswordCommand>
    {

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public ChangePasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;
            ChangePasswordUserRule();


        }
        public void ChangePasswordUserRule()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.CurrentPassword)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.NewPassword)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.NewPasswordConfirmation)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x)
                .Must(x => x.NewPassword == x.NewPasswordConfirmation)
                .WithMessage("Password Do not Match ");
        }


    }
}
