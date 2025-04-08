using PatronesComportamiento.Patrones;
using PatronesComportamiento.PatronesComportamiento.Patrones;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Sesion 05 - Patrones de comportamiento");

        AppStrategy.Ejecutar();

        Separador();

        AppTemplateMethod.Ejecutar();

        Separador();

        AppVisitor.Ejecutar();

        Separador();

        AppCommand.Ejecutar();

        Separador();

        PatronCommandMejorado.Ejecutar();

        Separador();

        AppChainOfResponsability.Ejecutar();
    }

    private static void Separador()
    {
        Thread.Sleep(2000);
        Console.WriteLine(new string('=', 50));
        Console.WriteLine();
    }
}