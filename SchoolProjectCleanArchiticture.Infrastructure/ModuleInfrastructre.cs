using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using SchoolProjectCleanArchiticture.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure
{
    public static  class ModuleInfrastructre
    {
        public static IServiceCollection AddServicesInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGernericRepos<>), typeof(GenericRepo<>));
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<ISubjectRepo, SubjectRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<ITeacherRepo, TeacherRepo>();
           services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
            
            return services;
        }
    }
}
