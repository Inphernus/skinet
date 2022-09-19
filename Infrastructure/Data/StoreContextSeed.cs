using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try 
            {
                if(!context.ProductBrands.Any())
                {
                    await SeedingProductBrandsAsync(context);
                }

                if(!context.ProductTypes.Any())
                {
                    await SeedingProductTypesAsync(context);
                }
                
                 if(!context.Products.Any())
                {
                    await SeedingProductsAsync(context);
                }
            }

            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.InnerException.Message);
            }
             
        }

        private static async Task SeedingProductBrandsAsync(StoreContext context)
        {
            var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            foreach (var item in brands)
            {
                context.ProductBrands.Add(item);
            }
            await context.SaveChangesAsync();
        }

         private static async Task SeedingProductTypesAsync(StoreContext context)
        {
            var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

            foreach (var item in types)
            {
                context.ProductTypes.Add(item);
            }
            await context.SaveChangesAsync();
        }
         private static async Task SeedingProductsAsync(StoreContext context)
        {
            var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            foreach (var item in products)
            {
                context.Products.Add(item);
            }
            await context.SaveChangesAsync();
        }
    }
}