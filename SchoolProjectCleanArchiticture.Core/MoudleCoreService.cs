using Microsoft.Extensions.DependencyInjection;
using SchoolProjectCleanArchiticture.Core.Mapping; 
using System.Reflection;
using FluentValidation;
using MediatR;
using AutoMapper;
using SchoolProjectCleanArchiticture.Core.Behaviors;
using Microsoft.AspNetCore.Http;

namespace SchoolProjectCleanArchiticture.Core
{
    public static class MoudleCoreService
    {
        public static IServiceCollection AddServicesCore(this IServiceCollection services)
        {
            
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

       
                services.AddAutoMapper(typeof(StudentProfile));
            

           
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
                 services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(typeof(DepartmentProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(RoleProfile));    
            services.AddAutoMapper(typeof(TeacherProfile));
                
           

            return services;
        }
    }
}
