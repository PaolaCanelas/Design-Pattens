using ShoppingCartApi.Dtos;

namespace ShoppingCartApi.Interfaces;

public interface ICartService
{
    Task<ShoppingCartDto> GetCartAsync(string userId);
    Task<ShoppingCartDto> AddItemAsync(string userId, AddItemRequest request);
    Task<ShoppingCartDto> RemoveItemAsync(string userId, int productId);
    Task<ShoppingCartDto> UpdateItemQuantityAsync(string userId, int productId, int quantity);
}