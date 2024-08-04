using Microsoft.Extensions.DependencyInjection;
using SchoolProjectCleanArchiticture.Services.Abstract;
using SchoolProjectCleanArchiticture.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services
{
    public static   class MouduleService
    {

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IDepartmentService
                , DepartmentService>();
            return services; 
        }
        
    }
}
