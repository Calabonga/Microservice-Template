using System;
using System.Linq;
using Calabonga.AspNetCore.Micro.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.DatabaseInitialization
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.Migrate();

                var roles = AppData.Roles.ToArray();

                foreach (var role in roles)
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
                    }
                }

                #region developer
                var developer1 = new ApplicationUser
                {
                    Email = "microservice@yopmail.com",
                    NormalizedEmail = "MICROSERVICE@YOPMAIL.COM",
                    UserName = "microservice@yopmail.com",
                    FirstName = "Microservice",
                    LastName = "Administrator",
                    NormalizedUserName = "MICROSERVICE@YOPMAIL.COM",
                    PhoneNumber = "+79000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                if (!context.Users.Any(u => u.UserName == developer1.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(developer1, "123123!");
                    developer1.PasswordHash = hashed;
                    var userStore = scope.ServiceProvider.GetService<ApplicationUserStore>();
                    var result = await userStore.CreateAsync(developer1);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException("Cannot create account");
                    }

                    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                    foreach (var role in roles)
                    {
                        var roleAdded = await userManager.AddToRoleAsync(developer1, role);
                        if (roleAdded.Succeeded)
                        {
                            await context.SaveChangesAsync();
                        }
                    }
                }
                #endregion

                await context.SaveChangesAsync();
            }
        }
    }
}