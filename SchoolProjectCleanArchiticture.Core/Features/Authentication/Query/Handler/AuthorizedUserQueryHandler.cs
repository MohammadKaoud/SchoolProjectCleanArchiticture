using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authentication.Query.Handler
{
    public class AuthorizedUserQueryHandler : ResponseHundler, IRequestHandler<AuthorizedUserQuery,ResponseM<string>>
    {
        private readonly UserManager<SUser> _userManager;

        private readonly SignInManager<SUser> _signInManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        public AuthorizedUserQueryHandler(UserManager<SUser> userManager, SignInManager<SUser> signInManager, IStringLocalizer<SharedResources> stringLocalizer, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;

        }

        public async Task<ResponseM<string>> Handle(AuthorizedUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            
            if(result== "TokenIsExpired")
            {
                return BadRequest<string>("Error This Token is Not Validate To give The  Access of Authorization");    
            }
            return Success($"This Token is Validate To Use {request.AccessToken}");
        }
    }
}
