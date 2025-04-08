using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Services;

public class PaymentProcessor : IPaymentProcessor
{
    private readonly ILogger<PaymentProcessor> _logger;

    public PaymentProcessor(ILogger<PaymentProcessor> logger)
    {
        _logger = logger;
    }

    public async Task<bool> ProcessPaymentAsync(ShoppingCart cart)
    {
        await Task.Delay(100);
        _logger.LogInformation("Se generó la orden para el {cart}", cart);

        return true;
    }
}