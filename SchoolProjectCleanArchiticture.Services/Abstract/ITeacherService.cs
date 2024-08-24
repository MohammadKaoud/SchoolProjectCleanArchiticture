using Microsoft.AspNetCore.Http;
using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public  interface ITeacherService
    {
        public Task<string> AddTeacherAsync(Teacher teacher,IFormFile Image);
    }
}
