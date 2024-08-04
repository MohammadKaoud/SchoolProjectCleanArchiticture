
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Hundlers
{
    public class DeleteStudentCommandHundler : IRequestHandler<DeleteStudentCommand, ResponseM<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentService studentService;
        private readonly IStringLocalizer<SharedResources> _strnigLocalizer;
        public DeleteStudentCommandHundler(IMapper mapper, IStudentService studentServices,IStringLocalizer<SharedResources>stringLocalizer)
        {
            
            _mapper = mapper;
            studentService = studentServices;
            _strnigLocalizer = stringLocalizer;
        }

        public async  Task<ResponseM<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var Response=new ResponseM<string>(); 
            var ResponseHundler=new ResponseHundler(_strnigLocalizer);
           var result= await studentService.DeleteAsync(request.Id);
            if (result == "Deleted")
            {
                Response.message = _strnigLocalizer[SharedResourcesKeys.Deleted];
                Response=ResponseHundler.Success(result);
                return Response;

            }
            Response = ResponseHundler.BadRequest<string>(_strnigLocalizer[SharedResourcesKeys.BadRequest]);
            return Response;
        }
    }
}
