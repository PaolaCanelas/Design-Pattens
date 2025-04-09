namespace Sesion06.Patrones;

public interface IState
{
    void Handle(Context context);
    string GetStateName();
}

// Estados concretos
public class StartState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("El sistema está iniciando...");
        // Cambiar al estado siguiente
        context.SetState(new RunningState());
    }

    public string GetStateName()
    {
        return "Start";
    }
}

public class RunningState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("El sistema está en ejecución...");
        // Si se cumple alguna condición, podríamos cambiar al estado de pausa
        context.SetState(new PausedState());
    }

    public string GetStateName()
    {
        return "Running";
    }
}

public class PausedState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("El sistema está en pausa...");
        // Podríamos volver al estado de ejecución o pasar a detener
        context.SetState(new StopState());
    }

    public string GetStateName()
    {
        return "Paused";
    }
}

public class StopState : IState
{
    public void Handle(Context context)
    {
        Console.WriteLine("El sistema se está deteniendo...");
        context.SetState(new StartState());
    }

    public string GetStateName()
    {
        return "Stop";
    }
}

public class Context
{
    private IState _currentState;

    public Context()
    {
        _currentState = new StartState();
    }

    public void SetState(IState state)
    {
        _currentState = state;
        Console.WriteLine($"Estado cambiado a: {state.GetStateName()}");
    }

    public void Request()
    {
        _currentState.Handle(this);
    }
}

public class AppState
{
    public static void Ejecutar()
    {
        Context context = new Context();

        context.Request();  // Start -> Running
        context.Request();  // Running -> Paused
        context.Request();  // Paused -> Stop
        context.Request();  // Stop -> Start
    }
}