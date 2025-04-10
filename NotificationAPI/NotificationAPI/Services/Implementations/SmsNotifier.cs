using NotificationAPI.Services.Abstractions;

namespace NotificationAPI.Services.Implementations;

public class SmsNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine($"SMS: {message.Substring(0, message.Length - 100)}");
    }
}