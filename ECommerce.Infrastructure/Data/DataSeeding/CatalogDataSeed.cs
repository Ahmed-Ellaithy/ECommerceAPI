using ECommerce.Domin.Contracts;
using ECommerce.Domin.Entities;
using ECommerce.Domin.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.DataSeeding
{
    public class CatalogDataSeed(StoreDbContext dbContext ) : IDataSeeder
    {

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                // check pending migrations and apply them
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync();
                //path
                var rootPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedDataIfEmptyAsync<ProductBrand, int>(rootPath, "brands.json", ct);
                await SeedDataIfEmptyAsync<ProductType, int>(rootPath, "types.json", ct);
                await SeedDataIfEmptyAsync<Product, int>(rootPath, "products.json", ct);

                var result = await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                {
                    //logger.LogInformation($" Data seeded successfully , {result} rows affected.");
                    Console.WriteLine($" Data seeded successfully , {result} rows affected.");
                }
                else
                    Console.WriteLine($" Failed to seed data.");
                //logger.LogInformation($" Failed to seed data.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //logger.LogError(ex.Message);
            }
        }


        // Method to read data from a JSON file and seed it into the database
        private async Task SeedDataIfEmptyAsync<T, TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            {
                return;
            }
            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                return;
            }
            // stream -- unmanaged  to stop it after reading the file we will use using statement
            using var fileStream = File.OpenRead(filePath);
            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream);
            if (items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);

        }
    }

}
