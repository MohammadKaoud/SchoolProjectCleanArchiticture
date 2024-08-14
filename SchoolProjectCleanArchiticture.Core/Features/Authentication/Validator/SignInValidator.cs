using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Features.Authentication.Model;
using SchoolProjectCleanArchiticture.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authentication.Validator
{
    public  class SignInValidator:AbstractValidator<SignIn>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public SignInValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;
            AddingUserRule();
        }
        public void AddingUserRule()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.Password).NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);




        }

    }
}
