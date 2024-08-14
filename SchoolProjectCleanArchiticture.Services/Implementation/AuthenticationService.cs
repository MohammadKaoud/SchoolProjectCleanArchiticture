using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using SchoolProjectCleanArchiticture.Infrastructure.Repos;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly UserManager<SchoolProjectCleanArchiticture.Data.Entites.Identity.SUser> _userManager;

        public AuthenticationService(IConfiguration configuration,JwtSettings jwtSettings,IRefreshTokenRepo refreshTokenRepo ,UserManager<SUser>userManager)
        {
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _userRefreshToken=new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepo = refreshTokenRepo;
            _userManager = userManager;
        }

        public async Task<JwtReponseAuth> GetJWTtoken(SUser sUser)
        {

            var (jwtToken, token) = GenerateSecurityToken(sUser);
           
            var RefreshToken=GetRefreshToken(sUser.UserName);
            //_userRefreshToken.AddOrUpdate(RefreshToken.RefreshedToken,RefreshToken,(s,t)=>RefreshToken);
            var ResponseWithTokenAuth = new JwtReponseAuth
            {
                RefreshToken = RefreshToken,
                AccessToken = token,
            };
            var RefreshTokenToSave = new UserRefreshToken
            {
                UserId = sUser.Id,
                IsRevoked=false,
                IsUsed=true,
                TimeAdded=DateTime.Now,
                ExpiredAt= DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiredAt),
                SUser = sUser,
                RefreshToken=RefreshToken.RefreshedToken,
                Token=ResponseWithTokenAuth.AccessToken,
                JwtId=jwtToken.Id,



            };
              var result=await _refreshTokenRepo.AddAsync(RefreshTokenToSave);

            
            return ResponseWithTokenAuth;
        }
        private  string  GenerateRefreshTokenstring()
        {
            var randomNumber =new Byte[32];
            var randomNumberGenerator=RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private RefreshToken GetRefreshToken(string userName) {

            var RefreshToken = new RefreshToken
            {
                UserName = userName,
                ExpiredAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiredAt),
                RefreshedToken =GenerateRefreshTokenstring(),
            };
            return RefreshToken;
        }
        public  List<Claim>GetUserClaims(SUser sUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(nameof(UserClaimsModel.PhoneNumber), sUser.PhoneNumber),
                new Claim(nameof(UserClaimsModel.Email), sUser.Email),
                new Claim(nameof(UserClaimsModel.UserName), sUser.UserName),
                new Claim(nameof(UserClaimsModel.Id), sUser.Id),
            };
            return claims;
        }
        private (JwtSecurityToken,string)GenerateSecurityToken(SUser sUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: GetUserClaims(sUser),
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtToken);
            return (jwtToken, token);
        }

        public async Task<JwtReponseAuth> GetRefreshToken(SUser userResponse,JwtSecurityToken token,string refreshToken,DateTime? ExpiredAt)
        {
            // ReadJwtToken
         
            var response = new JwtReponseAuth();
            var (jwtSecurityToken,newToken)= GenerateSecurityToken(userResponse);
            var refreshTokenResult = new RefreshToken()
            {
                ExpiredAt =(DateTime) ExpiredAt,
                UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value,
                RefreshedToken = refreshToken,

            };
            response.AccessToken=newToken;
            response.RefreshToken = refreshTokenResult;
            return response;

            

     
        }
        public  JwtSecurityToken ReadJwtSecurityToken(string accessToken)
        {
            if (!accessToken.IsNullOrEmpty())
            {
                var JwtTokenHandler = new JwtSecurityTokenHandler();
                var response = JwtTokenHandler.ReadJwtToken(accessToken);
                return response;
            }
            throw new NotImplementedException();
        }

        public Task<string> ValidateToken(string accessToken)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var response = JwtTokenHandler.ReadJwtToken(accessToken);
            var paramters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigninngkey,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
            };
           var validationResponse= JwtTokenHandler.ValidateToken(accessToken, paramters, out SecurityToken validatedToken);
            try
            {
                if (validatedToken == null)
                {
                    return Task.FromResult("TokenIsExpired");
                    throw new SecurityTokenArgumentException();

                }
                return Task.FromResult("Success");
            }
            catch (Exception ex)
            {

                return Task.FromResult("InvalidToken");
            }




        }

        public  async Task<(string,DateTime?)> ValidateDetails(JwtSecurityToken jwtSecurityToken, string accessToken, string refreshToken)
        {
            var token = ReadJwtSecurityToken(accessToken);
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("NotSameSecurityAlgorthim",null);
            }
            if (token.ValidTo < DateTime.UtcNow)
            {
                return ("AccessTokenHasBeenExpired", null);

            }
            var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
            var userResponse = await _userManager.FindByIdAsync(userId);
            if (userResponse == null)
            {
                return ("UserNotFound", null);

            }
            var userRefreshToken = _refreshTokenRepo.GetTableAsNoTracking().FirstOrDefault(
                x => x.RefreshToken == refreshToken && x.Token == accessToken &&
                x.SUser.Id == userResponse.Id

                );
            if (userRefreshToken.ExpiredAt < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                return ("refreshTokenhasBeenExpired", null);
            }
            return (userId,userRefreshToken.ExpiredAt);
        }
    }
}
