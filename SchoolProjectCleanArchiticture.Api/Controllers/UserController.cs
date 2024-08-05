using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;

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

    }
}
