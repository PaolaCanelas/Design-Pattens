using ShoppingCartApi.Models;

namespace ShoppingCartApi.Interfaces;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal subtotal, ShoppingCart cart);
}