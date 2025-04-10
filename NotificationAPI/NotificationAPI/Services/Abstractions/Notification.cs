namespace NotificationAPI.Services.Abstractions;

public abstract class Notification
{
    protected readonly INotifier Notifier;

    protected Notification(INotifier notifier)
    {
        Notifier = notifier;
    }

    public abstract void Send(string message);
}