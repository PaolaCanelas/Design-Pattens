using ShoppingCartApi.Dtos;

namespace ShoppingCartApi.Interfaces;

public interface ICheckoutService
{
    Task<BaseResponse> CheckoutAsync(string userId);
}