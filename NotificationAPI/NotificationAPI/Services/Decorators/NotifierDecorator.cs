using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Decorators;

public class NotifierDecorator : INotifier
{
    protected readonly INotifier Wrapped;

    public NotifierDecorator(INotifier wrapped)
    {
        Wrapped = wrapped;
    }

    public virtual void Notify(string message)
    {
        Wrapped.Notify(message);
    }
}