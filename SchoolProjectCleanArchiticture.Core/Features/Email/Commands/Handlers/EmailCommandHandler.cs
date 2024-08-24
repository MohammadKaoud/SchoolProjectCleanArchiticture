using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Email.Commands.Handlers
{
    public class EmailCommandHandler : ResponseHundler, IRequestHandler<SendEmailCommand, ResponseM<string>>
        ,IRequestHandler<ResetPasswordOperation,ResponseM<string>>
        ,IRequestHandler<ResetPasswordCommand, ResponseM<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResources>_stringLocalizer;
        private readonly Services.Abstract.IAuthenticationService _authenticationService;
        public EmailCommandHandler(IEmailService emailService,IStringLocalizer<SharedResources>stringLocalizer,Services.Abstract.IAuthenticationService authenticationService):base(stringLocalizer)
        {
            _emailService = emailService;
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        public async Task<ResponseM<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result =await _emailService.SendEmail(request.Email,request.Message,null);
            if (result == "Success")
            {
                return Success<string>(result);

            }
            return BadRequest<string>("Check The Service Layer When Authentication to Mail Service Was Failed");
        }

        public async Task<ResponseM<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
          var result=await _authenticationService.ResetPassword(request.Email);
            if(result== "CodeHasBeenSentSuccessfully")
            {
                return Success<string>(result); 
            }
            return BadRequest < string>("Something Went Wrong ");
        }

        public async Task<ResponseM<string>> Handle(ResetPasswordOperation request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPasswordOperation(request.Email, request.Code, request.Password);
            if (result == "Success")
            {
                return Success<string>(result);
            }
            return BadRequest<string>("Faild Check the Internal Service Logic Layer ");
        }
     
    }
}
