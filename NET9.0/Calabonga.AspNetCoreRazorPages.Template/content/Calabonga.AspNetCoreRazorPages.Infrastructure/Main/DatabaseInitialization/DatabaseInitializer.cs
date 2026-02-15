namespace Calabonga.AspNetCoreRazorPages.Infrastructure.Main.DatabaseInitialization
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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

            await context.SaveChangesAsync();
        }
    }
}
