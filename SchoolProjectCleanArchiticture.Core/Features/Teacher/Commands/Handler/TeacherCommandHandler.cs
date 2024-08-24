using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Teacher.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Teacher.Commands.Handler
{
    public  class TeacherCommandHandler:ResponseHundler,IRequestHandler<AddTeacherCommand,ResponseM<string>>

    {
        private readonly IStringLocalizer<SharedResources>_stringLocalizer;
    
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        public TeacherCommandHandler(IStringLocalizer<SharedResources>stringLocalizer,ITeacherService teacherService,IMapper mapper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
           
           _teacherService = teacherService;
            _mapper = mapper;
        }

        public async Task<ResponseM<string>> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
               var result=_mapper.Map<SchoolProjectCleanArchiticture.Data.Entites.Teacher>(request);
               var resultOfAddingTeacher=await  _teacherService.AddTeacherAsync(result, request.file);
                if (resultOfAddingTeacher == "Success")
                {
                    return Success<string>(resultOfAddingTeacher);
                }
                return BadRequest<string>("Check the Logic Service Layer for Internal Errors");
               
            }
            return BadRequest<string>("There Was Error While Fetching The Data From User");
        }
    }
}
