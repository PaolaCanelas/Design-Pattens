using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Services;

/// Aqui se aplica el Liskov Substituion Principle
public class DiscountStrategyFactory
{
    public IDiscountStrategy GetDiscountStrategy(ShoppingCart cart)
    {
        decimal subtotal = cart.Subtotal;

        // Aplicar diferentes estrategias basadas en reglas de negocio
        if (subtotal >= 100)
        {
            return new PremiumDiscount(100, 10); // 10% de descuento para compras mayores a $100
        }

        if (cart.Items.Count >= 5)
        {
            return new PercentageDiscount(5); // 5% de descuento por llevar más de 5 items
        }

        return new NoDiscount();
    }
}

public class NoDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal subtotal, ShoppingCart cart)
    {
        return subtotal;
    }
}

public class PercentageDiscount(decimal percentage) : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal subtotal, ShoppingCart cart)
    {
        return subtotal * (1 -percentage / 100);
    }
}

public class PremiumDiscount(decimal threshold, decimal percentage) : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal subtotal, ShoppingCart cart)
    {
        if (subtotal >= threshold)
        {
            return subtotal * (1 - percentage / 100);
        }

        return subtotal;
    }
}