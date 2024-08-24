using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Handlers
{
    public  class ClaimQueryHandler:ResponseHundler,IRequestHandler<ManageUserClaimQuery,ResponseM<ManageUserClaims>>
        ,IRequestHandler<UpdateUserClaimQuery,ResponseM<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources>_stringLocalizer;
        private readonly SchoolProjectCleanArchiticture.Services.Abstract.IAuthorizationService _authorizationService;
        private readonly UserManager<SUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ClaimQueryHandler(IMapper mapper,IStringLocalizer<SharedResources> stringLocalizer,Services.Abstract.IAuthorizationService authorizationService,UserManager<SUser>userManager,RoleManager<IdentityRole>roleManager):base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async  Task<ResponseM<ManageUserClaims>> Handle(ManageUserClaimQuery request, CancellationToken cancellationToken)
        {
         var user=await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
              var ManageResult=await   _authorizationService.ManageUserClaimsService(user);
                if(ManageResult != null)
                {
                    return Success<ManageUserClaims>(ManageResult);
                }
                return BadRequest<ManageUserClaims>("Something Went Wrong !!");
            }
            return BadRequest<ManageUserClaims>("User Not found ");  
        }

        public async Task<ResponseM<string>> Handle(UpdateUserClaimQuery request, CancellationToken cancellationToken)
        {
           var result=await _authorizationService.UpdateUserClaimsService(request);
            if(result != null)
            {
                return Success<string>(result); 
            }
            return BadRequest<string>("There Was A problem Inside the Logic Service Layer");
        }
    }
}
