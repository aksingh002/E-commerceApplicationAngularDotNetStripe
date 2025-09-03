using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    void AddProductsAsync(Product product);
    void UpdateProductsAsync(Product product);
    void DeleteProductsAsync(Product product);
    bool ProductExists(Product product);
    Task<bool> SaveChangesAsync();
}
