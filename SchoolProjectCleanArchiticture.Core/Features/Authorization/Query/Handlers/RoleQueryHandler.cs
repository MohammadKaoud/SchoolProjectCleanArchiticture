using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Dtos;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Handlers
{
    public class RoleQueryHandler :ResponseHundler, IRequestHandler<GetRolesQuery, ResponseM<List<GetRoleResult>>>
        ,IRequestHandler<GetRoleByIdQuery, ResponseM<GetRoleResult>>
        ,IRequestHandler<ManageUserRolesQuery,ResponseM<ManageUserRolesResult>>
        ,IRequestHandler<UpdateUserRolesCommand,ResponseM<string>>

    {
        private readonly IStringLocalizer<SharedResources>_stringLocalizer;
        private readonly Services.Abstract.IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<SUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,Services.Abstract.IAuthorizationService authorizationService,IMapper mapper,UserManager<SUser>userManager,RoleManager<IdentityRole>roleManager):base(stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async  Task<ResponseM<List<GetRoleResult>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetAllRole();
          
            var result= _mapper.Map<List<GetRoleResult>>(roles);
            if (result != null)
            {
              
                return Success<List<GetRoleResult>>(result);    
            }
            return BadRequest<List<GetRoleResult>>("Not Correct Mapping or There Is No  Roles ");
            
        }

        public async Task<ResponseM<GetRoleResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role=await _authorizationService.GetRoleById(request.Id);
            if(role != null)
            {
               var resultAfterMapping= _mapper.Map<GetRoleResult>(role);
                return Success<GetRoleResult>(resultAfterMapping);
            }
            return BadRequest<GetRoleResult>("Not Found This Specific Role ");
           
        }

        public async  Task<ResponseM<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user=await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
               var result=await  _authorizationService.ManageUserRoles(user);
                if (result != null)
                {
                    return Success<ManageUserRolesResult>(result);  
                }
                return BadRequest<ManageUserRolesResult>("Failed Fetch And View The Roles");
            }
            return BadRequest<ManageUserRolesResult>("User Not Found"); 
        }

        public async Task<ResponseM<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
         var result=await  _authorizationService.UpdateUserRolesService(request);
            if(result != null)
            {
                return Success<string>(result);
            }
            return BadRequest<string>("NotCompleteUpdatingRequest");
        }
    }
}
