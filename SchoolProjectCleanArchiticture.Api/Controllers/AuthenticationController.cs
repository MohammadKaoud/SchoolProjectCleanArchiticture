using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authentication.Model;
using SchoolProjectCleanArchiticture.Core.Features.Authentication.Query;
using SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models;

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
        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmail)
        {
            var result = await _mediator.Send(confirmEmail);
            return NewResult(result);
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
            return NewResult(result);
        }
        [HttpPost]
        [Route("ResetPasswordOperation")]
        public async Task<IActionResult> RsesetPasswordCheckValidation([FromQuery] ResetPasswordOperation resetPasswordOperationQuery)
        {
            var resut = await _mediator.Send(resetPasswordOperationQuery);
            return NewResult(resut);
        }

    }
}
