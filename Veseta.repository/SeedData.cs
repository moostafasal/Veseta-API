using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Veseta.Core.entites;
using Veseta.Core.entites.Enum;

namespace Veseta.API
{
    public static class SeedDataInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //await SeedRoles(roleManager);
            await SeedAdminUser(userManager);
        }

        //private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        //{
        //    string[] roleNames = { "Admin", "Doctor", "Patient" };

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExist)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }
        //}

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser

                { 
                    UserName = "admin",
                    FirstName="Zaki",
                    LastName="Psa",
                    Gender=Gender.Female,
                    Email = "admin@example.com",
                    DateOfBirth=DateTime.Now,

                };

                var createAdminUser = await userManager.CreateAsync(adminUser, "Admin@123");

                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    // Your additional seeding logic can go here if needed

                    Console.WriteLine("Admin user created successfully.");
                }
                else
                {
                    Console.WriteLine("Error creating admin user:");
                    foreach (var error in createAdminUser.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");
            }
        }
    }
}
