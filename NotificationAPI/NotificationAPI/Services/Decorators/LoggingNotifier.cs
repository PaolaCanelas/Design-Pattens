using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Decorators;

public class LoggingNotifier : NotifierDecorator
{
    public LoggingNotifier(INotifier wrapped) : base(wrapped)
    {
    }

    public override void Notify(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Enviando mensaje: {message}");
        base.Notify(message);
    }
}