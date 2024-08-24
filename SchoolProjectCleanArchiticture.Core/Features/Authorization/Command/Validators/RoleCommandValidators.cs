using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Validators
{
    public  class RoleCommandValidators:AbstractValidator<AddRoleCommand>
    {
        private readonly Services.Abstract.IAuthorizationService _authorizationService;
        public RoleCommandValidators(Services.Abstract.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            AddCasualValidation();
            AddCustomValidation();
        }
        public void AddCasualValidation()
        {
            RuleFor(x => x.RoleName).NotNull().WithMessage("Do not Left it Null");
        }
        public void AddCustomValidation()
        {
            RuleFor(x => x.RoleName).MustAsync(async (key, cancellationToken) => !await (_authorizationService.IsRoleExist(key))).WithMessage("RoleNameShouldNotBeNull");
        }
    }
}
