using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Command.Hundler
{
    public class AddUserHundler : IRequestHandler<AddUserCommand, ResponseM<string>>
        ,IRequestHandler<EditUserCommand,ResponseM<string>>
        ,IRequestHandler<ChangePasswordCommand,ResponseM<string>>
    {
        private IStringLocalizer<SharedResources>_stringLocalizer;
        private UserManager<SUser> _userManager;
        private IMapper _mapper;
        public AddUserHundler(IStringLocalizer<SharedResources>stringLocalizer,UserManager<SUser>userManager,IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;   
        }
        public async Task<ResponseM<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            ResponseHundler responseHundler = new ResponseHundler(_stringLocalizer);
            ResponseM<string> responseM = new ResponseM<string>();
           var response=  await _userManager.FindByEmailAsync(request.Email);
            if (response != null) return responseHundler.BadRequest<string>("This Email Has  Been registered With Before ");
            var responseWithName=await _userManager.FindByNameAsync(request.UserName);
            if (responseWithName != null) return responseHundler.BadRequest<string>("This UserName Has  Been registered With Before ");
            var userMapper = _mapper.Map<SUser>(request);
            var resultCreating=await _userManager.CreateAsync(userMapper,request.Password);
            if (resultCreating.Succeeded)
            {
               return   responseHundler.Success<string>("User have Been Addedd Okay");
            }
            return responseHundler.BadRequest<string>("There Was Something Wrong");

        }

        public async
            Task<ResponseM<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            ResponseHundler responseHundler = new ResponseHundler(_stringLocalizer);
            ResponseM<string> responseM = new ResponseM<string>();
            var response = await _userManager.FindByIdAsync(request.Id);
            if (response == null) return responseHundler.BadRequest<string>("There is no exist user With this UserName  ");
            var mapper= _mapper.Map(request,response);
             var result=await _userManager.UpdateAsync(mapper);
           
            if (result.Succeeded)
            {
                return responseHundler.Success<string>("complete Updating");
            }

            return responseHundler.BadRequest<string>("Not Complete Updating For Some reasons ");
        }

        public async Task<ResponseM<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            ResponseHundler responseHundler = new ResponseHundler(_stringLocalizer);
            ResponseM<string> responseM = new ResponseM<string>();
            var response = await _userManager.FindByIdAsync(request.Id);
           var resultOfChangingPassword=await   _userManager.ChangePasswordAsync(response,request.CurrentPassword,request.NewPassword);
            if (resultOfChangingPassword.Succeeded)
            {
                await _userManager.RemovePasswordAsync(response);
             var reulstofAddingPassword=  await  _userManager.AddPasswordAsync(response, request.NewPassword);
                if (reulstofAddingPassword.Succeeded)
                {
                    return responseHundler.Success<string>("Changing Password Operation has Been Completed ");
                }
                return responseHundler.BadRequest<string>("cannot Adding New Password For Some Reasons");
            }
            return responseHundler.BadRequest<string>("Enterd The Current Password Wrong Please Try Again ");

        }
    }
}
