using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public class ProductRepository(StoreContext storeContext) : IProductRepository
{


    public void AddProductsAsync(Product product)
    {
        storeContext.Products.Add(product);
    }

    public void DeleteProductsAsync(Product product)
    {
        storeContext.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await storeContext.Products.Select(x => x.Brand).Distinct().ToListAsync();
        
    }
    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await storeContext.Products.Select(x => x.Type).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand , string? type, string? sort)
    {
        var query = storeContext.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(x => x.Brand == brand);
        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(x => x.Type == type);
        if (!string.IsNullOrWhiteSpace(type))
        {
            query = sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };
        }
        return await query.ToListAsync();
    }

    public  bool ProductExists(int id )
    {
        return  storeContext.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }

    public void  UpdateProductsAsync(Product product)
    {
        storeContext.Entry(product).State = EntityState.Modified;
    }

}
