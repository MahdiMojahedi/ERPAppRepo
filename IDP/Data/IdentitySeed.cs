using Entities.Enums;
using IDP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class IdentitySeed
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            var roles = new[] { "Admin", "Coach", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"❌ Failed to create role '{role}':");
                        foreach (var error in result.Errors)
                            Console.WriteLine($"   • {error.Description}");
                    }
                }
            }

            // Seed Coach
            var coachEmail = "coach@example.com";
            if (await userManager.FindByEmailAsync(coachEmail) is null)
            {
                var coachUser = new ApplicationUser
                {
                    UserName = coachEmail,
                    Email = coachEmail,
                    Name = "Ali",
                    LastName = "Coach",
                    Gender = true,
                    Age = 35
                };

                var result = await userManager.CreateAsync(coachUser, "Coach@123");
                if (!result.Succeeded)
                {
                    Console.WriteLine("❌ Failed to create coach:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($"   • {error.Description}");
                }
                else
                {
                    await userManager.AddToRoleAsync(coachUser, "Coach");
                    Console.WriteLine("✅ Coach created");
                }
            }

            // Seed Student
            var studentEmail = "student@example.com";
            if (await userManager.FindByEmailAsync(studentEmail) is null)
            {
                var studentUser = new ApplicationUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    Name = "Sara",
                    LastName = "Student",
                    Gender = false,
                    Age = 22,
                };

                var result = await userManager.CreateAsync(studentUser, "Student@123");
                if (!result.Succeeded)
                {
                    Console.WriteLine("❌ Failed to create student:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($"   • {error.Description}");
                }
                else
                {
                    await userManager.AddToRoleAsync(studentUser, "Student");
                    Console.WriteLine("✅ Student created");
                }
            }

            // Seed Admin
            var adminEmail = "admin@example.com";
            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Admin",
                    LastName = "Super",
                    Gender = true,
                    Age = 40
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (!result.Succeeded)
                {
                    Console.WriteLine("❌ Failed to create admin:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($"   • {error.Description}");
                }
                else
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("✅ Admin created");
                }
            }
        }
    }

}
