namespace ShoppingCartApi.Dtos;

public class BaseResponse
{
    public string Message { get; set; } = null!;
    public string OrderId { get; set; } = null!;
    public bool Success { get; set; }
}