namespace ShoppingCartApi.Dtos;

public class ShoppingCartDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;

    public List<CartItemDto> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}