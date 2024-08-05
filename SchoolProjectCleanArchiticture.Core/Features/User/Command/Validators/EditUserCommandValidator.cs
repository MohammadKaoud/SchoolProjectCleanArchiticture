using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Validators
{
    public  class EditUserCommandValidator:AbstractValidator<EditUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public EditUserCommandValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;
            AddingUserRule();
        }
        public void AddingUserRule()
        {
      
            RuleFor(x => x.UserName).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.FullName).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
         


        }
    }
}
