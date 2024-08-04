using AutoMapper;
using SchoolProjectCleanArchiticture.Core.Features.Departments.Dtos;
using SchoolProjectCleanArchiticture.Data.Entites;

namespace SchoolProjectCleanArchiticture.Core.Mapping
{
    public partial class DepartmentProfile 
    {
      

        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subjects))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Name))
                .ForMember(dest => dest.Students, opt => opt.Ignore()) 
                .ReverseMap();

            CreateMap<Subject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubjectID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SubjectName))
                .ReverseMap();

            CreateMap<Teacher, TeacherResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            //CreateMap<Student, StudentResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LocalizedName))
            //    .ReverseMap();
        }
    }
}
