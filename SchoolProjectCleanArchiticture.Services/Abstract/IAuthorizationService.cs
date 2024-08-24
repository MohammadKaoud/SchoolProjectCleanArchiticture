using Microsoft.AspNetCore.Identity;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Dtos;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public  interface IAuthorizationService
    {
        public Task<bool>IsRoleExist(string roleName);
        public Task<string> CreateNewRole(string roleName);
        public Task<string> EditRole(EditRoleRequest request);
        public Task<bool> IsRoleExistById(string roleId);
        public Task<string>DeleteRoleById(string roleId);
        public Task<List<IdentityRole>> GetAllRole();
        public Task<IdentityRole> GetRoleById(string roleId);
        public Task<ManageUserRolesResult> ManageUserRoles(SUser user);
        public Task<string> UpdateUserRolesService(UpdateUserRolesRequest updateUserRolesRequest);
        public Task<ManageUserClaims>ManageUserClaimsService(SUser user);
        public Task<string>UpdateUserClaimsService(UpdateUserClaimsRequest updateUserClaimsRequest);
    }
}
