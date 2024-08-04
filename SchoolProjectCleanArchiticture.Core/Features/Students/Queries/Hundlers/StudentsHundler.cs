using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Dtos;
using SchoolProjectCleanArchiticture.Core.Features.Students.Queries.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Core.Wrapper;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Queries.Hundlers
{
    public  class StudentsHundler : IRequestHandler<GetStudentsQuery,ResponseM<IEnumerable<StudentDto>>>,
        IRequestHandler<GetSingleStudent,ResponseM<StudentDto>> ,IRequestHandler<GetPaginatedResult,PaginatedResult<StudentDto>>
    {
        private readonly  IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ILocalizationService _localizationService;
        public StudentsHundler(IStudentService studentService,IMapper mapper ,IStringLocalizer<SharedResources>stringLocalizer,ILocalizationService localizationService)
        {
            _studentService=studentService;
            _mapper=mapper;
            _stringLocalizer=stringLocalizer;
            _localizationService=localizationService;
        }

        public async  Task<ResponseM<IEnumerable<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students =  await _studentService.GetStudentsAsync();
            var studentsAfterMapping= _mapper.Map<IEnumerable<StudentDto>>(students);
            var responseHundler = new ResponseHundler(_stringLocalizer);
            var response=new ResponseM<IEnumerable<StudentDto>>();
            if (students is null)
            {
                response = responseHundler.NotFound<IEnumerable<StudentDto>>();
                return response;
            }
            response=responseHundler.Success<IEnumerable<StudentDto>>(studentsAfterMapping);
            response.Meta = new
            {

                Count = students.Count(),
               
                
            };
            return response;


           
        }

        public async Task<ResponseM<StudentDto>> Handle(GetSingleStudent request, CancellationToken cancellationToken)
        {
         var student=await _studentService.GetStudentByIdAsync(id: request.Id);
            var ResponseHundler=new ResponseHundler(_stringLocalizer);
            var Response=new ResponseM<StudentDto>();    
            if (student == null)
            {
                Response = ResponseHundler.NotFound<StudentDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                return Response;
            }
         var studentAfterMapping=_mapper.Map<StudentDto>(student);
            Response = ResponseHundler.Success<StudentDto>(studentAfterMapping);
            return Response;


        }

        
         Task<PaginatedResult<StudentDto>> IRequestHandler<GetPaginatedResult, PaginatedResult<StudentDto>>.Handle(GetPaginatedResult request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, StudentDto>> expression = e => new StudentDto(e.Id, e.LocalizedName, e.Address, e.Department.DepartmentName);
            var query = _studentService.GetAllStudentsQueryable();
            var paginatedResult = query.Select(expression).ToPaginatedResultAsync(request.PageNumber, request.PageSize);

            var queryWithSearchFunctionality = _studentService.GetAllStudentsQueryableSearch(request.OrderBy, request.Search);

            if (queryWithSearchFunctionality != null)
            {
                var searchResult = queryWithSearchFunctionality.Select(expression);
                paginatedResult = searchResult.ToPaginatedResultAsync(request.PageNumber, request.PageSize);
                paginatedResult.Meta = new
                {
                    count = paginatedResult._data.Count,
                    Message = "This Meta Contains The Number of data Stored inside the Paginated Result"
                };
            }

            return Task.FromResult(paginatedResult);
        }
    }
}
