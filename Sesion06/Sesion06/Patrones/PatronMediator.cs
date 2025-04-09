namespace Sesion06.Patrones;

// Interfaz del mediador
public interface IChatMediator
{
    void SendMessage(string message, User sender);

    void RegisterUser(User user);
}

// Clase abstracta para los usuarios
public abstract class User
{
    protected IChatMediator _mediator;
    protected string _name;

    public User(IChatMediator mediator, string name)
    {
        _mediator = mediator;
        _name = name;
    }

    public abstract void Send(string message);
    public abstract void Recieve(string message);
}

// Implementacion concreta del mediador
public class ChatRoom : IChatMediator
{
    private List<User> _users = new();

    public void SendMessage(string message, User sender)
    {
        foreach (var usuario in _users)
        {
            // No enviamos el mensaje al remitente
            if (usuario != sender)
            {
                usuario.Recieve(message);
            }
        }
    }

    public void RegisterUser(User user)
    {
        _users.Add(user);
    }
}

public class ChatUser : User
{
    public ChatUser(IChatMediator mediator, string name)
        : base(mediator, name)
    {
    }

    public override void Send(string message)
    {
        Console.WriteLine($"{_name} envía: {message}");
        _mediator.SendMessage(message, this);
    }

    public override void Recieve(string message)
    {
        Console.WriteLine($"{_name} recibe: {message}");
    }
}

public class AppMediator
{
    public static void Ejecutar()
    {
        var chatRoom = new ChatRoom();

        ChatUser user1 = new ChatUser(chatRoom, "Juanito");
        ChatUser user2 = new ChatUser(chatRoom, "Pepito");
        ChatUser user3 = new ChatUser(chatRoom, "Jaimito");

        chatRoom.RegisterUser(user1);
        chatRoom.RegisterUser(user2);
        chatRoom.RegisterUser(user3);

        user1.Send("Hola a todos");
        user2.Send("Hola Juanito");
    }
}