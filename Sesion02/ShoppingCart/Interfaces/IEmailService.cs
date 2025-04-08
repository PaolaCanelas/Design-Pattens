using ShoppingCartApi.Models;

namespace ShoppingCartApi.Interfaces;

public interface IEmailService
{
    Task SendConfirmationEmailAsync(string userId, ShoppingCart cart);
}