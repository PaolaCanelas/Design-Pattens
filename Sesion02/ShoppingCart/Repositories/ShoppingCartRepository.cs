using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Data;
using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ShoppingCartDbContext _dbContext;

    public ShoppingCartRepository(ShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ShoppingCart?> GetCartByUserIdAsync(string userId)
    {
        return await _dbContext.ShoppingCarts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId && !c.CheckedOut);
    }

    public async Task<ShoppingCart> CreateCartAsync(string userId)
    {
        var cart = new ShoppingCart { UserId = userId };
        await _dbContext.ShoppingCarts.AddAsync(cart);
        await _dbContext.SaveChangesAsync();
        return cart;
    }

    public async Task<ShoppingCart?> AddItemToCartAsync(int cartId, CartItem item)
    {
        var cart = await _dbContext.ShoppingCarts
            .Include(p => p.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is not null)
        {
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem is not null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }
        }

        return cart;
    }

    public async Task<ShoppingCart?> RemoveItemFromCartAsync(int cartId, int productId)
    {
        var cart = await _dbContext.ShoppingCarts
            .Include(p => p.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is not null)
        {
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem is not null)
            {
                cart.Items.Remove(existingItem);
                _dbContext.CartItems.Remove(existingItem);
            }
        }

        return cart;
    }

    public async Task<ShoppingCart?> UpdateItemQuantityAsync(int cartId, int productId, int quantity)
    {
        var cart = await _dbContext.ShoppingCarts
          .Include(p => p.Items)
          .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is not null)
        {
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem is not null)
            {
                existingItem.Quantity = quantity;
            }
        }

        return cart;
    }

    public async Task<ShoppingCart?> CheckoutCartAsync(int cartId)
    {
        var cart = await _dbContext.ShoppingCarts
            .Include(p => p.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is not null)
        {
            cart.CheckedOut = true;
            cart.CheckoutDate = DateTime.UtcNow;
        }

        return cart;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}