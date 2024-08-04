

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Core.Features.Students;
using SchoolProjectCleanArchiticture.Core.Features.Students.Queries.Models;
using SchoolProjectCleanArchiticture.Core.Dtos;
using Azure;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Api.Base;
using SchoolProjectCleanArchiticture.Infrastructure.Repos;
using SchoolProjectCleanArchiticture.Services.Abstract;

namespace SchoolProjectCleanArchiticture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ApplicationBaseController
    {
        


        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            
            var request = await _mediator.Send(new GetStudentsQuery());
            return NewResult( request);
        }
        [HttpGet]
        [Route("{StudentId:int}")]
        public async Task<IActionResult>GetStudentById(int StudentId)
        {
            var request = await _mediator.Send(new GetSingleStudent() { Id = StudentId });
            return NewResult(request);
        }
        [HttpPost]
        
        public async Task<IActionResult> AddStudent([FromBody]AddStudentCommand studentAddDto)
        {
            var request = await _mediator.Send(studentAddDto);
            return NewResult(request);
        }
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> EditStudent([FromBody]EditStudentCommand studentEditDto)
        {
            var requset=await _mediator.Send((studentEditDto));
            return NewResult(requset);
        }
        [HttpDelete]
        [Route("{StudentId:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int StudentId)
        {
            var request=await _mediator.Send(new DeleteStudentCommand() { Id=StudentId});
            return NewResult(request);
        }
        [HttpGet]
        [Route("Pagination")]
        public async Task<IActionResult> PaginatedResult([FromQuery] GetPaginatedResult query)
        {
            var request=await _mediator.Send(query);
            return Ok(request);
        }


    }
}
