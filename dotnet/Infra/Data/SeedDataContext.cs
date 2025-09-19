using System;
using System.Text.Json;
using Azure.Core.Serialization;
using Core.Entities;
using Infra.Config;

namespace Infra.Data;

public class SeedDataContext
{
    public static async Task SeedDataAsync(StoreContext storeContext)
    {
        if (!storeContext.Products.Any())
        {
            var Productjson = await File.ReadAllTextAsync("../Infra/SeedData/products.json");

            var product = JsonSerializer.Deserialize<List<Product>>(Productjson);

            if (product == null)
            {
                return;
            }

            await storeContext.Products.AddRangeAsync(product);

            await storeContext.SaveChangesAsync();
            

        }


    }
}
