using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCORE.Models;

namespace UserRegExample.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "SuperAdmin", "CustomerCare" };
            IdentityResult result;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    result = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            string Email = "superadmin@outlook.com";
            string Password = "Super@123987";
            if(userManager.FindByEmailAsync(Email).Result == null)//Yahan ek super admin ke liye Email & pass insert kara rahe hai
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = Email;
                user.Email = Email;//Kunki Identity Server ke ander User Name and Email dono same hi hote hain.
                IdentityResult resultIdentity = userManager.CreateAsync(user, Password).Result;
                if (resultIdentity.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }
        }
    }
}
//IServiceProvider roleManager ko add karne ka kaam karta hai