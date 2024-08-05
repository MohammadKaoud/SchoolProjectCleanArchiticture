using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Core.Features.User.Query.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Core.Wrapper;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.User.Query.Hundler
{
    public class UsersQueryHundler : IRequestHandler<GetPaginatedQuery, PaginatedResult<GetPaginatedUsers>>
        ,IRequestHandler<GetUserByNameQuery, ResponseM<UserToView>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper ;
        private readonly UserManager<SUser> _userManager;
        public UsersQueryHundler(IStringLocalizer<SharedResources> stringLocalizer,IMapper mapper,UserManager<SUser>userManager)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public Task<PaginatedResult<GetPaginatedUsers>> Handle(GetPaginatedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<SUser> allUsers = _userManager.Users.AsQueryable();
            var result = _mapper.ProjectTo<GetPaginatedUsers>(allUsers).ToPaginatedResultAsync(request.PageNumber, request.PageSize);
            ResponseHundler responseHundler = new ResponseHundler(_stringLocalizer);
            if (result != null)
            {
                return Task.FromResult(result);
            }
            return null;
        }

        public async  Task<ResponseM<UserToView>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var userTofind=_userManager.Users.FirstOrDefault(x=>x.UserName==request.UserName);
            ResponseHundler responseHundler = new ResponseHundler(_stringLocalizer);
            ResponseM<UserToView> responseM = new ResponseM<UserToView>();
            if (userTofind == null)
            {
                  responseM=responseHundler.NotFound<UserToView>("Not Found 404  ");
                return responseM;
                    
            }
           var mapper= _mapper.Map<UserToView>(userTofind);
            responseM = responseHundler.Success<UserToView>(mapper);
            return responseM;

        }
    }
}
