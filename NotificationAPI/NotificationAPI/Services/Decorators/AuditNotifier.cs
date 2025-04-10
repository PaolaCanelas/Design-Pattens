using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Decorators;

public class AuditNotifier : NotifierDecorator
{
    public AuditNotifier(INotifier wrapped) : base(wrapped)
    {
    }

    public override void Notify(string message)
    {
        Console.WriteLine($"[AUDITORIA] Notificacion registrada: {message}");
        base.Notify(message);
    }
}