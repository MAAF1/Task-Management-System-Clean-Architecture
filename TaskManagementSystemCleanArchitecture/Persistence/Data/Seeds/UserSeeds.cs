using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Data.Seeds
{
    public static class UserSeeds
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("SuperAdmin"))
                await  roleManager.CreateAsync(new ApplicationRole { Name = "SuperAdmin" });

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "User" });

            var superAdmin = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@task.com",
                EmailConfirmed = true
            };
            var user = await userManager.FindByEmailAsync(superAdmin.Email);
            if(user == null)
            {
                 var result = await userManager.CreateAsync(superAdmin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }
            }
        }


    }
}
