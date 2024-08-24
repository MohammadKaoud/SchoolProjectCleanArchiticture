using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolProjectCleanArchiticture.Infrastructure
{
    public  static  class IdentitySeeder
    {
      
     
        public async  static Task SeedUser(UserManager<SUser>_userManager)
        {
           var users=await _userManager.Users.ToListAsync();
            if (users.Count == 0)
            {
                SUser newUser = new SUser()
                {
                    FullName = "MohammadAhmadKaoud",
                    UserName = "MohammadKaoud",
                    Email = "mohammadkaoud5@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+96936769321",
                    Country = "Syria",
                    Address = "Mazzeh",
                    PhoneNumberConfirmed = true,

                };
               var CreatingResult=await _userManager.CreateAsync(newUser,"Suarez.123");
                if (CreatingResult.Succeeded)
                {
                    var resultOfAssignRole = await _userManager.AddToRoleAsync(newUser, "Admin");
                }
               
                
            }
        }
        public async static Task SeedRole(RoleManager<IdentityRole> _roleManager)
        {
            var roles=await _roleManager.Roles.ToListAsync();
            if (roles.Count == 0)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = "Admin"
                };
                IdentityRole SecondNewRole = new IdentityRole()
                {
                    Name = "User"
                };
               await  _roleManager.CreateAsync(newRole);
                await _roleManager.CreateAsync(SecondNewRole);
            }
        }
    }
}
