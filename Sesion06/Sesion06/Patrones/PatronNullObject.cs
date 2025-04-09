namespace Sesion06.Patrones;

// Interfaz comun
public interface ILogger
{
    void Log(string message);
}

// Implementacion real
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"$[LOG]: {message}");
    }
}

// Implementacion nula
public class NullLogger : ILogger
{
    public void Log(string message)
    {
       
    }
}


// Clase cliente que usa el Logger
public class Cliente
{
    private ILogger _logger;

    public Cliente(ILogger? logger = null)
    {
        _logger = logger ?? new NullLogger();
    }

    public void DoSomething()
    {
        
        _logger.Log("Haciendo algo");
        
    }
}

// Uso del patron Null Object
public class AppNullObject
{
    public static void Ejecutar()
    {
      
        ILogger logger = new ConsoleLogger();

        Cliente cliente = new(logger);
        cliente.DoSomething();

        Cliente cliente2 = new();
        cliente2.DoSomething();

    }
}