using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Implementations;

public class EmailNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine($"Mandamos el Email: {message}");
    }
}