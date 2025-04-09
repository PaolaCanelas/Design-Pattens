namespace Sesion06.Patrones;

// Interfaz para el observador
public interface IObserver
{
    void Update(ISubject subject);
}

// Interfaz para el sujeto observable
public interface ISubject
{
    void Attach(IObserver observer);
    void Detatch(IObserver observer);
    void Notify();
}

// Implementación concreta del sujeto
public class ConcreteSubject : ISubject
{
    private List<IObserver> _observers = new();
    private string _state;

    public string State
    {
        get => _state;
        set
        {
            _state = value;
            Notify(); // Notificar a los observadores cuando cambia el estado
        }
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detatch(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}

// Implementacion concreta del observador
public class ConcreteObserver : IObserver
{
    public string Name { get; set; }

    public ConcreteObserver(string name)
    {
        Name = name;
    }

    public void Update(ISubject subject)
    {
        var concreteSubject = (ConcreteSubject)subject;
        Console.WriteLine($"El nuevo estado es: {concreteSubject.State} del Observador: {Name}");
    }
}

// Ejemplo de uso
public class AppObserver
{
    public static void Ejecutar()
    {
        var subject = new ConcreteSubject();

        var observer1 = new ConcreteObserver("Observer 1");
        var observer2 = new ConcreteObserver("Observer 2");

        subject.Attach(observer1); // Suscribimos
        subject.Attach(observer2); // Suscribimos

        subject.State = "Estado 1";
        subject.State = "Estado 2";

        subject.Detatch(observer1);
        subject.State = "Estado 3";
    }
}