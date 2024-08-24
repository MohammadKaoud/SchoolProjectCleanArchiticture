using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Handlers
{
    public class RoleCommandHandlercs:ResponseHundler,IRequestHandler<AddRoleCommand,ResponseM<string>>
        ,IRequestHandler<EditRoleCommand,ResponseM<string>>
        ,IRequestHandler<DeleteRoleCommand,ResponseM<string>>   
    {
        private readonly Services.Abstract.IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public RoleCommandHandlercs(IStringLocalizer<SharedResources>stringLocalizer,Services.Abstract.IAuthorizationService authorizationService):base(stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer; 
            _authorizationService = authorizationService;
        }

        public async  Task<ResponseM<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result=await _authorizationService.CreateNewRole(request.RoleName);
            if (result == "Succeeded")
            {
               return Success(result+request.RoleName+"Has Been Created");
            }
            return BadRequest<string>("Faild Inside Creating The Role Check Internal Error");
        }

        public async Task<ResponseM<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRole(request);
            if(result == "Succeeded")
            {
                  return Success(result + request.Name + "Has Been Edit");
            }
            return BadRequest<string>("Not Correct Editing ");
        }

        public async Task<ResponseM<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var resut=await _authorizationService.DeleteRoleById(request.Id);
            if (resut == "Succeeded")
            {
                return Success("Role Has Been Deleted "); 
            }
            return BadRequest <string>("something Was Not Okay ");
        }
    }
}
