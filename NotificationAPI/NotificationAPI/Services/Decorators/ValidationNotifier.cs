using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Decorators;

public class ValidationNotifier : NotifierDecorator
{
    public ValidationNotifier(INotifier wrapped) : base(wrapped)
    {
    }

    public override void Notify(string message)
    {
        if (!message.Contains(","))
        {
            Console.WriteLine("[VALIDACION] No tiene una coma.");
            return;
        }

        Console.WriteLine("[VALIDACION] Validacion completa");

        base.Notify(message);
    }
}