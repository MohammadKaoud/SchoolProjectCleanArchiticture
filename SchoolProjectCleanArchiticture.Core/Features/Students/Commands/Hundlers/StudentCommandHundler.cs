using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Dtos;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Services.Abstract;
using SchoolProjectCleanArchiticture.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Hundlers
{
    public class StudentCommandHundler : IRequestHandler<AddStudentCommand, ResponseM<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
            
        public StudentCommandHundler(IMapper mapper,IStudentService studentService,IStringLocalizer<SharedResources>stringLocalizer)
        {
            _mapper=mapper;
            _studentService=studentService;
            _stringLocalizer=stringLocalizer;
        }
        public async Task<ResponseM<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var responseHundler = new ResponseHundler(_stringLocalizer  );
            var response = new SchoolProjectCleanArchiticture.Core.Base.ResponseM<string>();
            var student = _mapper.Map<Student>(request);
            var result= await  _studentService.AddStudentAsync(student);
            if (result == "exist")
            {
               response=  responseHundler.BadRequest<string>(result);
                return response;
               
            }
            response = responseHundler.Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
            return response;
        }
    }
}
