namespace NotificationAPI.Services.Adapters;

/// <summary>
/// Clase incomparable que representa un servicio de WhatsApp.
/// </summary>
public class WhatsAppService
{
    public void EnviarMensaje(string contenido)
    {
        Console.WriteLine($"[WhatsApp] {contenido}");
    }
}