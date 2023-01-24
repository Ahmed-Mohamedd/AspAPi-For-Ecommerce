using Microsoft.Extensions.Logging;
using SmartCart.DAl.Entities;
using SmartCart.DAl.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartCart.DAl.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory Loggerfactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var BrandsData = File.ReadAllText("../SmartCart.DAL/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductCategories.Any())
                {
                    var CategoriesData = File.ReadAllText("../SmartCart.DAL/Data/DataSeed/categories.json");
                    var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
                    foreach (var category in categories)
                    {
                        context.Set<ProductCategory>().Add(category);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../SmartCart.DAL/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.ReadAllText("../SmartCart.DAL/Data/DataSeed/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    foreach (var deliveryMethod in DeliveryMethods)
                    {
                        context.Set<DeliveryMethod>().Add(deliveryMethod);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                var logger = Loggerfactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
