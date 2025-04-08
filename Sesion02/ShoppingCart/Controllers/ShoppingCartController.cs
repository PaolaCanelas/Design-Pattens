using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Dtos;
using ShoppingCartApi.Exceptions;
using ShoppingCartApi.Interfaces;

namespace ShoppingCartApi.Controllers;


// Controlador que hace todo (violando el principio de responsabilidad única)
[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController(ICartService cartService) : ControllerBase
{
    // Obtener el carrito de un usuario
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        try
        {
            var cart = await cartService.GetCartAsync(userId);
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Agregar un producto al carrito
    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddItem(string userId, [FromBody] AddItemRequest request)
    {
        try
        {
            var cart = await cartService.AddItemAsync(userId, request);
            return Ok(cart);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Eliminar un item del carrito
    [HttpDelete("{userId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(string userId, int productId)
    {
        try
        {
            var cart = await cartService.RemoveItemAsync(userId, productId);
            return Ok(cart);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Actualizar cantidad de un item
    [HttpPut("{userId}/items/{productId}")]
    public async Task<IActionResult> UpdateItemQuantity(string userId, int productId, [FromBody] UpdateQuantityRequest request)
    {
        try
        {
            var cart = await cartService.UpdateItemQuantityAsync(userId, productId, request.Quantity);
            return Ok(cart);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}