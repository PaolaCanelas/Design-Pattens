using ShoppingCartApi.Data;
using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShoppingCartDbContext _dbContext;

    public ProductRepository(ShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task UpdateStockAsync(int productId, int quantityChange)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        if (product is not null)
        {
            product.Stock -= quantityChange;
            await _dbContext.SaveChangesAsync();
        }
    }
}