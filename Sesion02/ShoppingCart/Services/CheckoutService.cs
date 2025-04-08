using ShoppingCartApi.Dtos;
using ShoppingCartApi.Exceptions;
using ShoppingCartApi.Interfaces;

namespace ShoppingCartApi.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IOrderGenerator _orderGenerator;
    private readonly IEmailService _emailService;

    public CheckoutService(IShoppingCartRepository cartRepository,
        IPaymentProcessor paymentProcessor,
        IOrderGenerator orderGenerator,
        IEmailService emailService)
    {
        _cartRepository = cartRepository;
        _paymentProcessor = paymentProcessor;
        _orderGenerator = orderGenerator;
        _emailService = emailService;
    }

    public async Task<BaseResponse> CheckoutAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null) throw new NotFoundException("Carrito no encontrado");
        if (!cart.Items.Any()) throw new BadRequestException("El carrito está vacío");

        bool success = await _paymentProcessor.ProcessPaymentAsync(cart);
        if (!success)
            throw new BadRequestException("Error al procesar el pago");

        await _cartRepository.CheckoutCartAsync(cart.Id);
        await _cartRepository.SaveChangesAsync();

        await _orderGenerator.GenerateOrderAsync(cart);

        await _emailService.SendConfirmationEmailAsync(userId, cart);

        return new BaseResponse()
        {
            Message = "Compra finalizada correctamente",
            OrderId = Guid.NewGuid().ToString(),
            Success = true
        };

    }
}