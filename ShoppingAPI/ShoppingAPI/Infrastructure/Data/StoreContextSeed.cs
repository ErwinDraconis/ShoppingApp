using Microsoft.Extensions.Logging;
using ShoppingAPI.Core.Entities;
using ShoppingAPI.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingAPI.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerfactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText(@"..\shoppingAPI\Infrastructure\Data\SeedData\brands.json");
                    var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var item in brand)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText(@"..\shoppingAPI\Infrastructure\Data\SeedData\types.json");
                    var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    foreach (var item in type)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\shoppingAPI\Infrastructure\Data\SeedData\products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach (var item in product)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "Cannot create Database");
            }
        }
    }
}
