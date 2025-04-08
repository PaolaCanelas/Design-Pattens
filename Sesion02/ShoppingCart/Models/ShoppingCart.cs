namespace ShoppingCartApi.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    public decimal TotalAmount { get; set; }
    public decimal Subtotal => Items.Sum(item => item.Subtotal);
    public bool CheckedOut { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CheckoutDate { get; set; }
}