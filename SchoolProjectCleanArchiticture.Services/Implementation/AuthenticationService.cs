﻿using Microsoft.AspNetCore.Identity;
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
using Microsoft.AspNetCore.Hosting;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly UserManager<SchoolProjectCleanArchiticture.Data.Entites.Identity.SUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
       
        public AuthenticationService(IConfiguration configuration, JwtSettings jwtSettings, IRefreshTokenRepo refreshTokenRepo, UserManager<SUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepo = refreshTokenRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<JwtReponseAuth> GetJWTtoken(SUser sUser)
        {

            var (jwtToken, token) = await GenerateSecurityToken(sUser);

            var RefreshToken = GetRefreshToken(sUser.UserName);
            //_userRefreshToken.AddOrUpdate(RefreshToken.RefreshedToken,RefreshToken,(s,t)=>RefreshToken);
            var ResponseWithTokenAuth = new JwtReponseAuth
            {
                RefreshToken = RefreshToken,
                AccessToken = token,
            };
            var RefreshTokenToSave = new UserRefreshToken
            {
                UserId = sUser.Id,
                IsRevoked = false,
                IsUsed = true,
                TimeAdded = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiredAt),
                SUser = sUser,
                RefreshToken = RefreshToken.RefreshedToken,
                Token = ResponseWithTokenAuth.AccessToken,
                JwtId = jwtToken.Id,



            };
            var result = await _refreshTokenRepo.AddAsync(RefreshTokenToSave);


            return ResponseWithTokenAuth;
        }
        private string GenerateRefreshTokenstring()
        {
            var randomNumber = new Byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private RefreshToken GetRefreshToken(string userName)
        {

            var RefreshToken = new RefreshToken
            {
                UserName = userName,
                ExpiredAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiredAt),
                RefreshedToken = GenerateRefreshTokenstring(),
            };
            return RefreshToken;
        }
        public async Task<List<Claim>> GetUserClaims(SUser sUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(nameof(UserClaimsModel.PhoneNumber), sUser.PhoneNumber),
                new Claim(nameof(UserClaimsModel.Email), sUser.Email),
                new Claim(nameof(UserClaimsModel.UserName), sUser.UserName),
                new Claim(nameof(UserClaimsModel.Id), sUser.Id),
            };
            var roles = await _userManager.GetRolesAsync(sUser);

            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role.ToString());
                claims.Add(roleClaim);
            }
            var userClaims = await _userManager.GetClaimsAsync(sUser);
            if (userClaims is not null)
            {
                claims.AddRange(userClaims);
            }
            return claims;
        }
        private async Task<(JwtSecurityToken, string)> GenerateSecurityToken(SUser sUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: await GetUserClaims(sUser),
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtToken);
            return (jwtToken, token);
        }

        public async Task<JwtReponseAuth> GetRefreshToken(SUser userResponse, JwtSecurityToken token, string refreshToken, DateTime? ExpiredAt)
        {
            // ReadJwtToken

            var response = new JwtReponseAuth();
            var (jwtSecurityToken, newToken) = await GenerateSecurityToken(userResponse);
            var refreshTokenResult = new RefreshToken()
            {
                ExpiredAt = (DateTime)ExpiredAt,
                UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value,
                RefreshedToken = refreshToken,

            };
            response.AccessToken = newToken;
            response.RefreshToken = refreshTokenResult;
            return response;




        }
        public JwtSecurityToken ReadJwtSecurityToken(string accessToken)
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
            var validationResponse = JwtTokenHandler.ValidateToken(accessToken, paramters, out SecurityToken validatedToken);
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

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtSecurityToken, string accessToken, string refreshToken)
        {
            var token = ReadJwtSecurityToken(accessToken);
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("NotSameSecurityAlgorthim", null);
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
            return (userId, userRefreshToken.ExpiredAt);
        }

        public async Task<string> ConfirmEmail(string UserId, string Code)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null || Code == null)
            {
                return "userNotFound Or Code Field is Empty";
            }
            var result = await _userManager.ConfirmEmailAsync(user, Code);
            if (result.Succeeded)
            {
                return "Succeeded";
            }
            return "Wrong Or Icorrect Validation To Email Service";
        }

        public async Task<string> ResetPassword(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var CodeToGenerate = new Random();
                var CodeToPlace = CodeToGenerate.Next(1000000);

                user.Code = CodeToPlace.ToString();
                var resultofUpdate = await _userManager.UpdateAsync(user);
                if (resultofUpdate.Succeeded)
                {
                    var result = await _emailService.SendEmail(Email, $"Your Code Please Put it To Reset your password :{CodeToPlace}", "Reset Password");
                    if (result == "Success")
                    {
                        return "CodeHasBeenSentSuccessfully";
                    }
                }
                return "Problem When Updating the User";

            }
            return "UserNotFound";
        }

        public async Task<string> ResetPasswordOperation(string Email, string Code, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                if (user.Code == Code)
                {
                    var resultOfDeletingOldPassword = await _userManager.RemovePasswordAsync(user);
                    if (resultOfDeletingOldPassword.Succeeded)
                    {
                        var resultofUpdatingPassword = await _userManager.AddPasswordAsync(user, Password);
                        if (resultofUpdatingPassword.Succeeded)
                        {
                            return "Success";
                        }
                        return "faild While Updating the new Password";
                    }
                    return "Faild While Remove The Old Password";

                }
                return "Code Is Not Equal";

            }
            return "User Not Found";



        }
    }
}
