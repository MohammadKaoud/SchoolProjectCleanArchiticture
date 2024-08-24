using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Core.Features.Teacher.Commands.Models;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ApplicationBaseController
    {
        [HttpPost]
        [Route("AddingNewTeacher")]
       public async Task<IActionResult> AddNewTeacher([FromForm]AddTeacherCommand addTeacherCommand)
        {
            var result=await _mediator.Send(addTeacherCommand);
            return NewResult(result);
        }
    }
}
