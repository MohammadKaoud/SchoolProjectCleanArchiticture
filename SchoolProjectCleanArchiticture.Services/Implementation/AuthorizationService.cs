using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Dtos;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<SUser> _userManager;
        public AuthorizationService(RoleManager<IdentityRole>roleManager,UserManager<SUser>userManager)
        {
               _roleManager = roleManager;

            _userManager = userManager;
        }
        public async Task<string> CreateNewRole(string roleName)
        {
            IdentityRole role=new IdentityRole(roleName);
            if (!roleName.IsNullOrEmpty())
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return "Succeeded";
                }
                return "failed";
            }
            return "failed";
        }

        public async  Task<string> DeleteRoleById(string roleId)
        {
            if (roleId == null)
            {
                return "ErrorMust be Not Null";
            }
           var result=await  IsRoleExistById(roleId);
            if (result == true)
            {
                var roleToDelete = await _roleManager.FindByIdAsync(roleId);
                var UsersRelatedToThisRole=await _userManager.GetUsersInRoleAsync(roleToDelete.Name);
                if (UsersRelatedToThisRole.Count() > 0)
                {

                    return "UsedByUsers";
                }
            
                var DeleteResult=await _roleManager.DeleteAsync(roleToDelete);
                if (DeleteResult.Succeeded)
                {
                    return "Succeeded";
                }
            }
           return  "returnNotFound";
        }

        public async Task<string> EditRole(EditRoleRequest request)
        {
          var roleToEdit=await _roleManager.FindByIdAsync(request.Id);
            if (roleToEdit != null)
            {
                roleToEdit.Name = request.Name;
                var result=await _roleManager.UpdateAsync(roleToEdit);
                if (result.Succeeded) {

                    return "Succeeded";
                }
                return "Failed";
            }
            return "NotFound";
        }

        public async Task<List<IdentityRole>> GetAllRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<IdentityRole> GetRoleById(string roleId)
        {
           var roleToCheck=await _roleManager.FindByIdAsync(roleId);
            if (roleToCheck != null)
            {
                return roleToCheck;
            }
            return null;
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            var result = await _roleManager.FindByNameAsync(roleName);
            if (result == null)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> IsRoleExistById(string roleId)
        {
            var result = await _roleManager.FindByIdAsync(roleId);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ManageUserClaims> ManageUserClaimsService(SUser user)
        {
            if (user != null)
            {
              var userClaims=await   _userManager.GetClaimsAsync(user);
                if(userClaims != null)
                {
                    ManageUserClaims manageUserClaims = new ManageUserClaims();
                    manageUserClaims.UserId = user.Id;
                    List<UserClaim> claims = new List<UserClaim>();
                    foreach (var claim in ClaimStore.ClaimsData)
                    {
                        UserClaim userClaim = new UserClaim();
                        userClaim.Type = claim.Type;
                        if (userClaims.Any(x => x.Type == claim.Type)){
                            userClaim.Value = true;
                        }
                        else
                        {
                            userClaim.Value =false;
                        }
                        claims.Add(userClaim);
                    }
                    manageUserClaims.UserClaims = claims;
                    return manageUserClaims;
                }
                return null; 
            }
            return null;
            
        }

        public async  Task<ManageUserRolesResult> ManageUserRoles(SUser user)
        {
            if (user != null)
            {
                var allRoles = await _roleManager.Roles.ToListAsync();
                var userRoles=await  _userManager.GetRolesAsync(user);
                ManageUserRolesResult result = new ManageUserRolesResult();
                result.Id = user.Id;
                List<UserRole> userRolesVM = new List<UserRole>();
                if (allRoles.Count > 0)
                {
                    foreach (var role in allRoles)
                    {
                        UserRole userRole = new UserRole();
                        userRole.Id = role.Id;
                        userRole.Name = role.Name;
                        if (userRoles.Contains(role.Name.ToString()))
                        {
                            userRole.hasRole = true;
                        }
                        else
                        {
                            userRole.hasRole= false;
                        }
                        userRolesVM.Add(userRole);

                    }
                }
               result.UserRoles = userRolesVM;
            return result;
                
            }
            return null;
    
        }

        public async Task<string> UpdateUserClaimsService(UpdateUserClaimsRequest updateUserClaimsRequest)
        {
          var user=await _userManager.FindByIdAsync(updateUserClaimsRequest.UserId.ToString());
            if (user != null)
            {

                var userClaims = await _userManager.GetClaimsAsync(user);
                if (userClaims != null)
                {
                    var removeClaimsResut = await _userManager.RemoveClaimsAsync(user, userClaims);

                    if (removeClaimsResut.Succeeded)
                    {
                        var claimsTocheck = updateUserClaimsRequest.UserClaims.Where(x => x.Value == true).Select(x => new Claim(type: x.Type, value: x.Value.ToString()));
                        foreach (var claim in claimsTocheck)
                        {
                            var resultUpdating = await _userManager.AddClaimAsync(user, claim);
                            if (resultUpdating.Succeeded)
                            {
                              
                            }
                        }
                        return "Complete Updating the Claims For User";
                    }
                }
                return "something Went Wrong When Delete the Old User Claims";
                
              
            }
            return "UserNotFound";
        }

        public async Task<string> UpdateUserRolesService(UpdateUserRolesRequest updateUserRolesRequest)
        {
          var user=await _userManager.FindByIdAsync(updateUserRolesRequest.Id.ToString());
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                //Remove userRoles
               var RemoveResult=await  _userManager.RemoveFromRolesAsync(user, userRoles);
                if (RemoveResult.Succeeded)
                {
                    var rolesToUpdate=updateUserRolesRequest.UserRoles.Where(x=>x.hasRole==true).ToList();
                    foreach (var role in rolesToUpdate)
                    {
                       var resultofAddingToSpecificRole=await  _userManager.AddToRoleAsync(user,role.Name.ToString());
                        if (resultofAddingToSpecificRole.Succeeded)
                        {
                            role.hasRole = true;
                        }
                    }
                    return "CompleteUpdatingUserRoles";
                }
                return "Removing Was Facing A problem ";
            }
            return "userNotFound";
        }
       
    }
}
