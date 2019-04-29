using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.AspNetCore.MicroModule.Data.DatabaseInitialization
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

                // TODO: Add your seed data here

                await context.SaveChangesAsync();
            }
        }
    }
}