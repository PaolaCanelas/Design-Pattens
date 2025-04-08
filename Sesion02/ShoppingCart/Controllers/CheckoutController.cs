using Microsoft.AspNetCore.Mvc;
using ShoppingCartApi.Exceptions;
using ShoppingCartApi.Interfaces;

namespace ShoppingCartApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController(ICheckoutService checkoutService) : ControllerBase
{
    [HttpPost("{userId}")]
    public async Task<IActionResult> Checkout(string userId)
    {
        try
        {
            var result = await checkoutService.CheckoutAsync(userId);
            return Ok(result);
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