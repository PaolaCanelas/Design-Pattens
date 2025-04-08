using ShoppingCartApi.Models;

namespace ShoppingCartApi.Interfaces;

public interface IOrderGenerator
{
    Task GenerateOrderAsync(ShoppingCart cart);
}