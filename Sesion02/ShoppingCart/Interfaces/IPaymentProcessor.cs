using ShoppingCartApi.Models;

namespace ShoppingCartApi.Interfaces;

public interface IPaymentProcessor
{
    Task<bool> ProcessPaymentAsync(ShoppingCart cart);
}