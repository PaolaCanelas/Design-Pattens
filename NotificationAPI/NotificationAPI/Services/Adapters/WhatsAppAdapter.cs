using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Adapters;

public class WhatsAppAdapter : INotifier
{
    private readonly WhatsAppService _service = new();

    public void Notify(string message)
    {
        _service.EnviarMensaje(message);
    }
}