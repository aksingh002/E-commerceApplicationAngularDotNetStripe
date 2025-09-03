using System;
using Core.Entities;
using Core.Interfaces;

namespace Core.Data;

public class ProductRepository : IProductRepository
{
    public void AddProductsAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public void DeleteProductsAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public bool ProductExists(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public void UpdateProductsAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
