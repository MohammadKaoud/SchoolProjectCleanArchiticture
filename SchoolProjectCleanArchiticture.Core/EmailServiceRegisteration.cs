using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProjectCleanArchiticture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core
{
    public static  class EmailServiceRegisteration
    {
        public static IServiceCollection EmailServiceReg(this IServiceCollection services,IConfiguration configuration)
        {
            var EmailSettings = new EmailSettings();
            configuration.GetSection("EmailSettings").Bind(EmailSettings);
            services.AddSingleton(EmailSettings);
            return services;   

        }
    }
}
