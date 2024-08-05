using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Query.Models;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApplicationBaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewAccount([FromBody] AddUserCommand addUserCommand)
        {
            var result = await  _mediator.Send(addUserCommand);
            return NewResult(result);
        }
        [HttpGet]
        [Route("UsersPaginatedResult")]
        public async Task<IActionResult> GetPaginatedUser([FromQuery]GetPaginatedQuery getPaginatedQuery)
        {
            var result=await _mediator.Send(getPaginatedQuery);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetSingleUser")]
        public async Task<IActionResult> GetUserByIdAsync( string UserName)
        {
            var result = await _mediator.Send(new GetUserByNameQuery(UserName));
            return NewResult(result);
        }


    }
}
