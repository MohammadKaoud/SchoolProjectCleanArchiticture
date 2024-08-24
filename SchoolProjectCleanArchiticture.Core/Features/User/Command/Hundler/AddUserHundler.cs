using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Identity.Client;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Hundler
{
    public class AddUserHundler :ResponseHundler,IRequestHandler<AddUserCommand, ResponseM<string>>
    {
        private readonly  IStringLocalizer<SharedResources>_stringLocalizer;
        private  readonly UserManager<SUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly Services.Abstract.IAuthorizationService _authorizationService;
        private readonly IUrlHelper _urlHelper;

        public AddUserHundler(IStringLocalizer<SharedResources>stringLocalizer,UserManager<SUser>userManager,IMapper mapper,Services.Abstract.IAuthorizationService authorizationService,IHttpContextAccessor httpContextAccessor,IEmailService emailService,IUrlHelper urlHelper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;   
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;
        }
        public async Task<ResponseM<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // ...

            // Create user
            var userMapper = _mapper.Map<SUser>(request);
            var resultCreating = await _userManager.CreateAsync(userMapper, request.Password);
            if (!resultCreating.Succeeded)
            {
                return BadRequest<string>("There Was Something Wrong");
            }

            // Assign role
            if (request.Role != null && await _authorizationService.IsRoleExist(request.Role))
            {
                await _userManager.AddToRoleAsync(userMapper, request.Role);
            }
            else
            {
                await _userManager.AddToRoleAsync(userMapper, "User");
            }

            // Generate email confirmation token
            var codeToConfirmOperation = await _userManager.GenerateEmailConfirmationTokenAsync(userMapper);
            var encodedCode = WebUtility.UrlEncode(codeToConfirmOperation);
            string protocol = _httpContextAccessor.HttpContext.Request.Scheme;
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var portNumber = _httpContextAccessor.HttpContext.Request.Host.Port;
            string returnedUrl = $"{protocol}://{host}:{portNumber}" +
                                 _urlHelper.Action("ConfirmEmail", "Authentication",
                                 new { userId = userMapper.Id, code = codeToConfirmOperation });
            var res = $"{protocol}://{host}/api/Authentication/ConfirmEmail?UserId={userMapper.Id}&Code={encodedCode}";
        

            // Format the returnedUrl as a hyperlink
            string emailBody = $"Please confirm your account by clicking <a href='{returnedUrl}'>here</a>";
            var sendingEmailResult = await _emailService.SendEmail(userMapper.Email, res,reason:"Email Confirmation");
            if (sendingEmailResult != "Success")
            {
                return BadRequest<string>("Error sending email");
            }

            // Return success response, don't check if email is confirmed
            return Success<string>("User Have Been Added Okay");
        }
    }
}
