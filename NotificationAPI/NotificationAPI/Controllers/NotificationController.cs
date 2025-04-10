using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Services.Abstractions;
using NotificationAPI.Services.Adapters;
using NotificationAPI.Services.Decorators;
using NotificationAPI.Services.Implementations;

namespace NotificationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotifier _notifier;

    public NotificationController(INotifier notifier)
    {
        _notifier = notifier;
    }

    [HttpPost("email")]
    public IActionResult SendEmail(string message)
    {
        var notification = new AlertNotification(new EmailNotifier());
        notification.Send(message); 
        return Ok("Endpoint ejecutado.");
    }

    [HttpPost("sms")]
    public IActionResult SendSms(string message)
    {
        var notification = new AlertNotification(new SmsNotifier());
        notification.Send(message);
        return Ok("Endpoint SMS ejecutado.");
    }

    [HttpPost("whatsapp")]
    public IActionResult SendWhatsApp(string message)
    {
        INotifier notifier = new WhatsAppAdapter();
        var notification = new AlertNotification(notifier);
        notification.Send(message);
        return Ok("WhatsApp enviado");
    }

}