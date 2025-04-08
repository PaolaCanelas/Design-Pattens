using ShoppingCartApi.Dtos;
using ShoppingCartApi.Exceptions;
using ShoppingCartApi.Interfaces;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Services;

public class CartService : ICartService
{
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartRepository _cartRepository;
    private readonly DiscountStrategyFactory _discountFactory;

    public CartService(IProductRepository productRepository, IShoppingCartRepository cartRepository,
        DiscountStrategyFactory discountFactory)
    {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _discountFactory = discountFactory;
    }

    public async Task<ShoppingCartDto> GetCartAsync(string userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null)
        {
            cart = await _cartRepository.CreateCartAsync(userId);
        }

        return await CalculateCartTotalAsync(cart);
    }

    public async Task<ShoppingCartDto> AddItemAsync(string userId, AddItemRequest request)
    {
        // Validar stock
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null) throw new NotFoundException("Producto no encontrado");

        if (product.Stock < request.Quantity)
            throw new BadRequestException("No hay suficiente stock disponible");

        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null)
        {
            cart = await _cartRepository.CreateCartAsync(userId);
        }

        var newItem = new CartItem()
        {
            ProductId = product.Id,
            Product = product,
            Quantity = request.Quantity,
            UnitPrice = product.Price
        };

        await _cartRepository.AddItemToCartAsync(cart.Id, newItem);

        await _productRepository.UpdateStockAsync(product.Id, request.Quantity);

        await _cartRepository.SaveChangesAsync();

        return await CalculateCartTotalAsync(cart);

    }

    public async Task<ShoppingCartDto> RemoveItemAsync(string userId, int productId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null) throw new NotFoundException("Carrito no encontrado");

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null) throw new NotFoundException("Producto no encontrado en el carrito");

        await _productRepository.UpdateStockAsync(productId, -item.Quantity);

        await _cartRepository.RemoveItemFromCartAsync(cart.Id, productId);
        await _cartRepository.SaveChangesAsync();

        return await CalculateCartTotalAsync(cart);
    }

    public async Task<ShoppingCartDto> UpdateItemQuantityAsync(string userId, int productId, int quantity)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart is null) throw new NotFoundException("Carrito no encontrado");

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null) throw new NotFoundException("Producto no encontrado en el carrito");

        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new NotFoundException("Producto no encontrado");

        var stockDifference = quantity - item.Quantity;
        if (stockDifference > 0 && product.Stock < stockDifference)
            throw new BadRequestException("No hay suficiente stock disponible");

        await _productRepository.UpdateStockAsync(productId, stockDifference);

        await _cartRepository.UpdateItemQuantityAsync(cart.Id, productId, quantity);
        await _cartRepository.SaveChangesAsync();

        return await CalculateCartTotalAsync(cart);
    }

    private async Task<ShoppingCartDto> CalculateCartTotalAsync(ShoppingCart cart)
    {
        var discountStrategy = _discountFactory.GetDiscountStrategy(cart);

        var subtotal = cart.Subtotal;
        var totalAmount = discountStrategy.ApplyDiscount(subtotal, cart);
        var discount = subtotal - totalAmount;

        // Mapear a DTO
        var cartDto = new ShoppingCartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = cart.Items.Select(item => new CartItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product?.Name ?? "Producto desconocido",
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Subtotal = item.Subtotal
            }).ToList(),
            Subtotal = subtotal,
            Discount = discount,
            TotalAmount = totalAmount
        };

        return await Task.FromResult(cartDto);
    }
}