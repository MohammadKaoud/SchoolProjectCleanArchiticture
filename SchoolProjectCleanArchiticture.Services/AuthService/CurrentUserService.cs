using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolProjectCleanArchiticture.Services.AuthService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor  _httpContextAccessor;
        private readonly UserManager<SUser> _userManager;  
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<SUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async  Task<SUser> GetUserAsync()
        {
            var userId=await GetUserIdAsync();
          var  user=await   _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return user;  
            }
            return null;

        }

        public  async Task<string> GetUserIdAsync()
        {
            var userId=_httpContextAccessor.HttpContext.User.Claims.First(x=>x.Type==nameof(UserClaimsModel.Id)).Value;
            return userId;
        }

        public async  Task<List<string>> GetUserRolesAsync(SUser sUser)
        {
           var roles=await _userManager.GetRolesAsync(sUser);
            return roles.ToList();
        }
    }
}
