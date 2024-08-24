using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Authentication.Model;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAuthenticationService = SchoolProjectCleanArchiticture.Services.Abstract.IAuthenticationService;

namespace SchoolProjectCleanArchiticture.Core.Features.Authentication.Handler
{
    public  class SignInHandler: ResponseHundler,IRequestHandler<SignIn,ResponseM<JwtReponseAuth>>
        ,IRequestHandler<Model.RefreshToken,ResponseM<JwtReponseAuth>>
        ,IRequestHandler<ConfirmEmailQuery,ResponseM<string>>
    
    {
        private readonly UserManager<SUser> _userManager;

        private readonly SignInManager<SUser> _signInManager;
        private readonly IStringLocalizer<SharedResources>_stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        public SignInHandler(UserManager<SUser>userManager,SignInManager<SUser>signInManager,IStringLocalizer<SharedResources> stringLocalizer,IAuthenticationService authenticationService):base(stringLocalizer)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;

        }

        public async Task<ResponseM<JwtReponseAuth>> Handle(SignIn request, CancellationToken cancellationToken)
        {
         
           var user=await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return NotFound<JwtReponseAuth>("User Not Found");
            }
            var result=await  _signInManager.CheckPasswordSignInAsync(user, request.Password,false);
            if (result.Succeeded)
            {
                if (!user.EmailConfirmed)
                {
                    return BadRequest<JwtReponseAuth>("Email Is  Not Confirmed ");
                }
                //Generate TokenJWT
               var jwttoken=  await _authenticationService.GetJWTtoken(user);
                return Success<JwtReponseAuth>(jwttoken);
                
            }
            return BadRequest<JwtReponseAuth>("Not Complete Operation ");
        }

        public async Task<ResponseM<JwtReponseAuth>> Handle(Model.RefreshToken request, CancellationToken cancellationToken)
        {
           // ReadTheToken
           var token=_authenticationService.ReadJwtSecurityToken(request._accessToken);
            // ValidateSomeDetials
            var validationResult= await _authenticationService.ValidateDetails(token,request._accessToken,request._refreshToken);
            ResponseM<JwtReponseAuth> result = new ResponseM<JwtReponseAuth>();
            switch (validationResult)
            {
                case  ("NotSameSecurityAlgorthim", null) :
                     result=Unauthorized<JwtReponseAuth>();
                    result.message = "NotSameAlgorthim";
                    return result;

                case ("AccessTokenHasBeenExpired", null):
                    result = Unauthorized<JwtReponseAuth>();
                    result.message = "AccessTokenHasBeenExpired";
                    return result;
                case ("UserNotFound", null):
                    var resultToreturn = Unauthorized<JwtReponseAuth>();
                    resultToreturn.message = "UserNotFound";
                    return resultToreturn;
                case ("refreshTokenhasBeenExpired", null):
                    result = Unauthorized<JwtReponseAuth>();
                    result.message = "refreshTokenhasBeenExpired";
                    return result;
               


            }
            var (userId, ExpiredAt) = validationResult;
            
            var user= await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                result=BadRequest<JwtReponseAuth>("There is No Exist User in System");
                return result;
            }
            var responseToView =await _authenticationService.GetRefreshToken(user,token,request._refreshToken,ExpiredAt);
            return Success(responseToView);

        }

        public async Task<ResponseM<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
           var resultofConfirmation =await  _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if(resultofConfirmation== "Succeeded")
            {
                return Success<string>(resultofConfirmation);
            }
            return BadRequest<string>("Check the Internal Server Error or inside the logic Service layer ");
        }
       
    }
}
