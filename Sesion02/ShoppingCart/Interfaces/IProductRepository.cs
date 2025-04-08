using ShoppingCartApi.Models;

namespace ShoppingCartApi.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);

    Task UpdateStockAsync(int productId, int quantityChange);
}

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> GetCartByUserIdAsync(string userId);

    Task<ShoppingCart> CreateCartAsync(string userId);

    Task<ShoppingCart?> AddItemToCartAsync(int cartId, CartItem item);

    Task<ShoppingCart?> RemoveItemFromCartAsync(int cartId, int productId);
    Task<ShoppingCart?> UpdateItemQuantityAsync(int cartId, int productId, int quantity);

    Task<ShoppingCart?> CheckoutCartAsync(int cartId);

    Task SaveChangesAsync();
}