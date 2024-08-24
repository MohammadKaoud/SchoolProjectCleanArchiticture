using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Departments.Dtos;
using SchoolProjectCleanArchiticture.Core.Features.Departments.Queries.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Core.Wrapper;
using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Data.Views;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System.Linq.Expressions;

public class GetDepartmentByIdHandler : ResponseHundler,IRequestHandler<GetDepartmentByIdQuery, ResponseM<GetDepartmentByIdDto>>
    ,IRequestHandler<GetDepartmentViewQuery,ResponseM<List<DepartmentView>>>
{
    private readonly IMapper _mapper;
    private readonly IDepartmentService _departmentService;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IStudentService _studentService;

    public GetDepartmentByIdHandler(IMapper mapper, IDepartmentService departmentService, IStringLocalizer<SharedResources> stringLocalizer, IStudentService studentService):base(stringLocalizer)
    {
        _mapper = mapper;
        _departmentService = departmentService;
        _stringLocalizer = stringLocalizer;
        _studentService = studentService;
    }

    public async Task<ResponseM<GetDepartmentByIdDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
        var responseHandler = new ResponseHundler(_stringLocalizer);
        var response = new ResponseM<GetDepartmentByIdDto>();

        if (department == null)
        {
            response = responseHandler.NotFound<GetDepartmentByIdDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            return response;
        }

        var result = _mapper.Map<GetDepartmentByIdDto>(department);
        if (result == null)
        {
            response = responseHandler.NotFound<GetDepartmentByIdDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            return response;
        }

        Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.Id, e.LocalizedName);

        var studentQuery = _studentService.GetQueryableStudentWithRelatedDepartment(request.Id);
        var studentPaginated =  studentQuery.Select(expression).ToPaginatedResultAsync(request.StudentPageNumber, request.StudentPageSize);

        result.Students = studentPaginated;
        response = responseHandler.Success<GetDepartmentByIdDto>(result);
        return response;
    }

    public async Task<ResponseM<List<DepartmentView>>> Handle(GetDepartmentViewQuery request, CancellationToken cancellationToken)
    {
        var result = await _departmentService.GetDepartmentView();
        if (result != null)
        {
            return Success<List<DepartmentView>>(result);
        }
        return BadRequest<List<DepartmentView>>("Something went wrong While Fetch The Data");
    }
}
