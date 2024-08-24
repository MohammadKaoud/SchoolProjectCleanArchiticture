using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ApplicationBaseController
    {
        [HttpPost]
        [Route("SendingEmail")]
        public async Task<IActionResult> SendEmail([FromQuery]SendEmailCommand sendEmailCommand)
        {
            var resut = await _mediator.Send(sendEmailCommand);
            return NewResult(resut);
        }
      
    }
}
