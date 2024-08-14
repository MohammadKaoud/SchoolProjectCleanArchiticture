using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authentication.Model;
using SchoolProjectCleanArchiticture.Core.Features.Authentication.Query;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApplicationBaseController
    {

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignInUser([FromForm]SignIn signIn)
        {
            var result=await _mediator.Send(signIn);
            return NewResult(result);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm]RefreshToken refreshToken)
        {
            var result=await _mediator.Send(refreshToken);
            return NewResult(result);
        }
        [HttpGet]
        [Route("AuthorizeResult")]
        public async Task<IActionResult> GetAuthorizedUserToken([FromQuery]AuthorizedUserQuery authorizedUserQuery)
        {
            var result=await _mediator.Send(authorizedUserQuery);
            return NewResult(result );
        }

    }
}
