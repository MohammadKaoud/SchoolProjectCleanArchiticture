using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Api.Base;

using SchoolProjectCleanArchiticture.Core.Features.Departments.Queries.Models;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ApplicationBaseController
    {
        [HttpGet]
        [Route("GetDepartmentById")]
      
        public async Task<IActionResult> GetDepartmentById([FromQuery]GetDepartmentByIdQuery getDepartmentByIdQuery)
        {
            var result=await _mediator.Send(getDepartmentByIdQuery);
            return NewResult(result);

        }
        
    }
}
