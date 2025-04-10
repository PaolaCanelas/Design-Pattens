using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Implementations;

public class AlertNotification : Notification
{
    public AlertNotification(INotifier notifier) : base(notifier)
    {
    }

    public override void Send(string message)
    {
        Notifier.Notify($"[ALERTA] {message}");
    }
}