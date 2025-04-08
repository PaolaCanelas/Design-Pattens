using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Repositories;
using ShoppingCartApi.Services;

namespace ShoppingCartApi;

public static class DependencyInjection
{
    public static IServiceCollection AddShoppingCartServices(this IServiceCollection services)
    {
        // Registro de repositorios
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

        // Registro de servicios
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IPaymentProcessor, PaymentProcessor>();
        services.AddScoped<IOrderGenerator, OrderGenerator>();
        services.AddScoped<IEmailService, EmailService>();

        // Registro de estrategias
        services.AddSingleton<DiscountStrategyFactory>();

        return services;
    }
}