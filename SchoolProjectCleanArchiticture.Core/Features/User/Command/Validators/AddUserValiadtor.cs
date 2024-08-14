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
    public  class AddUserValiadtor:AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer ;
        public AddUserValiadtor(IStringLocalizer<SharedResources> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;
            AddingUserRule();
        }
        public void AddingUserRule()
        {
            RuleFor(x => x.Password).Equal(x => x.PasswordConfirmation).WithMessage("The password Confiirmation not Equal To Password written ");
            RuleFor(x => x.UserName).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.FullName).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.Email).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.Password).MaximumLength(15).WithMessage("The Limit is 15 Characters Specific For Password ");

            
        
        }
    }
}
