using Microsoft.AspNetCore.Identity;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.AuthService
{
    public  interface ICurrentUserService
    {
        public Task<SUser> GetUserAsync();
        public Task<string> GetUserIdAsync();
        public Task<List<string>> GetUserRolesAsync(SUser sUser);
    }
}
