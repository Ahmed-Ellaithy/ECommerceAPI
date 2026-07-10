using ECommerce.Domin.Contracts;

namespace ECommerce.API
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            //unmanaged resource -- we will use using statement to dispose it after using it
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");

            await seeder.SeedDataAsync();
            return app;

        }

    }
}
