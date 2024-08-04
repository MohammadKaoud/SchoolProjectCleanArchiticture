using Microsoft.Extensions.DependencyInjection;
using SchoolProjectCleanArchiticture.Core.Mapping; 
using System.Reflection;
using FluentValidation;
using MediatR;
using AutoMapper;
using SchoolProjectCleanArchiticture.Core.Behaviors;

namespace SchoolProjectCleanArchiticture.Core
{
    public static class MoudleCoreService
    {
        public static IServiceCollection AddServicesCore(this IServiceCollection services)
        {
            
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

       
                services.AddAutoMapper(typeof(StudentProfile));
            

           
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           
                 services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(typeof(DepartmentProfile));
                
           

            return services;
        }
    }
}
