using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public  interface IAuthenticationService
    {
        public Task< JwtReponseAuth> GetJWTtoken(SUser sUser);
        public Task<JwtReponseAuth> GetRefreshToken(SUser sUser,JwtSecurityToken jwtSecurityToken,string RefreshToken,DateTime? ExpiredAt); 
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJwtSecurityToken(string accessToken);
        public Task<(string,DateTime?)>ValidateDetails(JwtSecurityToken jwtSecurityToken,string accessToken,string refreshTokeb);
        public Task<string> ConfirmEmail(string UserId, string Code);
        public Task<string> ResetPassword(string Email);
        public Task<string> ResetPasswordOperation(string Email, string Code,string password);
      

    }
}
