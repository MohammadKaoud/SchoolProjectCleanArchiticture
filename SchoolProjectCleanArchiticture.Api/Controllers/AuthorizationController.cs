using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Command.Models;
using SchoolProjectCleanArchiticture.Core.Features.Authorization.Query.Models;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ApplicationBaseController
    {
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return NewResult(result);
        }
        [HttpGet]
        [Route("GetRoleById{RoleId}")]
        public async Task<IActionResult> GetRoleById(string RoleId)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(RoleId));
            return NewResult(result);
        }

        [HttpPost]
        [Route("CreatingNewRole")]
        public async Task<IActionResult> AddNewRole([FromForm] string RoleName)
        {
            var result = await _mediator.Send(new AddRoleCommand(RoleName));
            return NewResult(result);
        }

        [HttpPut]
        [Route("EditRole")]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand editRoleCommand)
        {
            var result = await _mediator.Send(editRoleCommand);
            return NewResult(result);
        }

        [HttpDelete]
        [Route("{RoleId}")]
        public async Task<IActionResult> DeleteRole([FromRoute] string RoleId)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(RoleId));
            return NewResult(result);
        }
        [HttpGet]
        [Route("ManageUserRolesByUserId {UserId}")]
        public async Task<IActionResult> ManagerUserRoles(string UserId)
        {
            var result = await _mediator.Send(new ManageUserRolesQuery(UserId));
            return NewResult(result);
        }
        [HttpPut]
        [Route("UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles([FromBody]UpdateUserRolesCommand updateUserRolesCommand)
        {
            var result = await _mediator.Send(updateUserRolesCommand);
            return NewResult(result);
        }
        [HttpGet]
        [Route("ManageUserClaimsBy{UserId}")]
        public async Task<IActionResult> ManageUserClaims([FromRoute]string UserId)
        {
            var result = await _mediator.Send(new ManageUserClaimQuery(UserId));
            return NewResult(result);
        }
        [HttpPut]
        [Route("UpdateUserClaims")]
        public async Task<IActionResult> UpdateUserClaims([FromBody]UpdateUserClaimQuery updateUserClaimQuery)
        {
            var result = await _mediator.Send(updateUserClaimQuery);
            return NewResult(result);
        }
    }
}
