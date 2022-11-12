using $ext_projectname$.Domain;
using $ext_projectname$.Domain.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$.DatabaseInitialization;

/// <summary>
/// Database Initializer
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    /// Seeds one default users to database for demo purposes only
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static async void SeedUsers(IServiceProvider serviceProvider)
    {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            // ATTENTION!
            // -----------------------------------------------------------------------------
            // This is should not be used when UseInMemoryDatabase()
            // It should be uncomment when using UseSqlServer() settings or any other providers.
            // -----------------------------------------------------------------------------
            //await context!.Database.EnsureCreatedAsync();
            //var pending = await context.Database.GetPendingMigrationsAsync();
            //if (pending.Any())
            //{
            //    await context!.Database.MigrateAsync();
            //}

            if (context.Users.Any())
            {
                return;
            }

            var roles = AppData.Roles.ToArray();

            foreach (var role in roles)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                if (!context!.Roles.Any(r => r.Name == role))
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
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ApplicationUserProfile = new ApplicationUserProfile
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = "SEED",
                    Permissions = new List<AppPermission>
                {
                    new()
                    {
                        CreatedAt = DateTime.Now,
                        CreatedBy = "SEED",
                        PolicyName = "EventItems:UserRoles:View",
                        Description = "Access policy for EventItems controller user view"
                    }
                }
                }
            };

            if (!context!.Users.Any(u => u.UserName == developer1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(developer1, "123qwe!@#");
                developer1.PasswordHash = hashed;
                var userStore = scope.ServiceProvider.GetRequiredService<ApplicationUserStore>();
                var result = await userStore.CreateAsync(developer1);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Cannot create account");
                }

                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                foreach (var role in roles)
                {
                    var roleAdded = await userManager!.AddToRoleAsync(developer1, role);
                    if (roleAdded.Succeeded)
                    {
                        await context.SaveChangesAsync();
                    }
                }
            }

            #endregion

            await context.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds one event to database for demo purposes only
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static async void SeedEvents(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        // ATTENTION!
        // -----------------------------------------------------------------------------
        // This is should not be used when UseInMemoryDatabase()
        // It should be uncomment when using UseSqlServer() settings or any other providers.
        // -----------------------------------------------------------------------------
        //await context!.Database.EnsureCreatedAsync();
        //var pending = await context.Database.GetPendingMigrationsAsync();
        //if (pending.Any())
        //{
        //    await context!.Database.MigrateAsync();
        //}

        if (context.EventItems.Any())
        {
            return;
        }

        await context.EventItems.AddAsync(new EventItem
        {
            CreatedAt = DateTime.UtcNow,
            Id = Guid.Parse("1467a5b9-e61f-82b0-425b-7ec75f5c5029"),
            Level = "Information",
            Logger = "SEED",
            Message = "Seed method some entities successfully save to ApplicationDbContext"
        });
    }
}