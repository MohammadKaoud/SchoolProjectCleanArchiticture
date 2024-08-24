using FluentValidation;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Validators
{
    public  class EditRoleValidator:AbstractValidator<EditRoleCommand>
    {
        private readonly Services.Abstract.IAuthorizationService _authorizationService;
        public EditRoleValidator(Services.Abstract.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            AddCasualValidation();
            AddCustomValidation();
        }
        public void AddCasualValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Do not Left it Null");
        }
        public void AddCustomValidation()
        {
            RuleFor(x => x.Name).MustAsync(async (key, cancellationToken) => !await (_authorizationService.IsRoleExist(key))).WithMessage("RoleNameShouldNotBeNull");
        }

    }
}
