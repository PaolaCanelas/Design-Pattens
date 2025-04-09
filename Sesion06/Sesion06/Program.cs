
using Sesion06.Patrones;

Console.WriteLine("Patrones de Comportamiento - Parte 2: Sesion 06");

Console.WriteLine("Seleccione el patron a ejecutar:");

Console.WriteLine("1. Patrón Observer");
Console.WriteLine("2. Patrón Observer Mejorado");
Console.WriteLine("3. Patron Mediator");
Console.WriteLine("4. Patrón Null Object");
Console.WriteLine("5. Patron Iterator");
Console.WriteLine("6. Patron Interpreter");
Console.WriteLine("7. Patron Memento");
Console.WriteLine("8. Patron State");

var opcion = Console.ReadLine();

switch (opcion)
{
    case "1":
        Console.WriteLine("Patrón Observador");
        AppObserver.Ejecutar();
        break;
    case "2":
        Console.WriteLine("Patrón Observador Mejorado");
        AppObserverMejorado.Ejecutar();
        break;
    case "3":
        Console.WriteLine("Patrón Mediator");
        AppMediator.Ejecutar();
        break;
    case "4":
        Console.WriteLine("Patrón Null Object");
        AppNullObject.Ejecutar();
        break;
    case "5":
        Console.WriteLine("Patrón Iterator");
        AppIterator.Ejecutar();
        break;
    case "6":
        Console.WriteLine("Patrón Interpreter");
        AppInterpreter.Ejecutar();
        break;
    case "7":
        Console.WriteLine("Patrón Memento");
        AppMemento.Ejecutar();
        break;
    case "8":
        Console.WriteLine("Patrón State");
        AppState.Ejecutar();
        break;
    default:
        Console.WriteLine("Opcion no valida");
        break;
}