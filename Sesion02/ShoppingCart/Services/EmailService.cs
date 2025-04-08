using ShoppingCartApi.Dtos;
using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }
    public async Task SendConfirmationEmailAsync(string userId, ShoppingCart cart)
    {
        await Task.Delay(100);

        _logger.LogInformation("Se envió correo electrónico {userId} {cart}", userId, cart);
    }
}