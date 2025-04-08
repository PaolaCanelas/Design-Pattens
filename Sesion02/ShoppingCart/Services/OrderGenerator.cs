using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Services;

public class OrderGenerator : IOrderGenerator
{
    private readonly ILogger<OrderGenerator> _logger;

    public OrderGenerator(ILogger<OrderGenerator> logger)
    {
        _logger = logger;
    }

    public async Task GenerateOrderAsync(ShoppingCart cart)
    {
        await Task.Delay(100);
        _logger.LogInformation("Se generó la orden para el {cart}", cart);
    }
}