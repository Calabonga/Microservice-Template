namespace Calabonga.Microservice.Module.Infrastructure.DatabaseInitialization
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            // It should be uncomment when using UseSqlServer() settings or any other providers.
            // This is should not be used when UseInMemoryDatabase()
            // await context!.Database.MigrateAsync();

            await context!.EventItems.AddAsync(new EventItem
            {
                CreatedAt = DateTime.UtcNow,
                Id = Guid.Parse("1467a5b9-e61f-82b0-425b-7ec75f5c5029"),
                Level = "Information",
                Logger = "SEED",
                Message = "Seed method some entities successfully save to ApplicationDbContext"
            });

            await context.SaveChangesAsync();
        }
    }
}