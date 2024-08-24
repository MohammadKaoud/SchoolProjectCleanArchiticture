using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolProjectCleanArchiticture.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Filter
{
    public class AuthenticationActionFilter : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        public AuthenticationActionFilter(IHttpContextAccessor httpContextAccessor,ICurrentUserService currentUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;   
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = await _currentUserService.GetUserAsync();
            if (user!= null)
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var roles = await _currentUserService.GetUserRolesAsync(user);
                    if (roles.All(x => x != "User"))
                    {
                        ObjectResult result = new ObjectResult(StatusCodes.Status403Forbidden);
                    }
                    else
                    {
                        await next();
                    }
                }
            }
        }
    }
}
