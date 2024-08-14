using MediatR;
using SchoolProjectCleanArchiticture.Core.Base;
using SchoolProjectCleanArchiticture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Authentication.Model
{
    public class RefreshToken:IRequest<ResponseM<JwtReponseAuth>>
    {
        public string _accessToken { get; set; }
        public string _refreshToken {  get; set; }
    }
}
