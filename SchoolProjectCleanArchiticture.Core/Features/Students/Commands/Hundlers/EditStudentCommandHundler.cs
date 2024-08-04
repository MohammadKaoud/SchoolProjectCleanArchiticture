
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
    public class EditStudentCommandHundler : IRequestHandler<EditStudentCommand, ResponseM<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentService studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public EditStudentCommandHundler(IMapper mapper,IStudentService studentServices,IStringLocalizer<SharedResources> stringLocalizer)
        {
             _mapper = mapper;
            studentService = studentServices;
            _stringLocalizer = stringLocalizer;
        }


        public async  Task<ResponseM<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {

            // mapper
            ResponseHundler responseHundler = new ResponseHundler(_stringLocalizer);
            
            ResponseM<string> response = new ResponseM<string>(); 
            var student=_mapper.Map<Student>(request);
           var result= await studentService.UpdateAsync(student);
            if (result.Equals("c"))
            {
                response.Data= result.ToString();  
               response= responseHundler.Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                return response;
            }
            response.Data= result.ToString();
            responseHundler.BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
            return response;


        }
    }
}
