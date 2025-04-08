namespace PatronesComportamiento.Patrones;

/*
Escenario: Tenemos una orden de compra que pasa por diferentes estapads de aprobacion. Cada etapa de aprobacion 
(por ejemplo, aprobación por el supervisor, por el gerente, finalmente por el director).
Cada nivel de aprobación decide si maneja la orden o la pasa al siguiente nivel.
*/

// 1. Definir la interfaz Handler
public abstract class Aprobador
{
    protected Aprobador _siguienteAprobador = null!;

    public void SetSiguienteAprobador(Aprobador siguienteAprobador)
    {
        _siguienteAprobador = siguienteAprobador;
    }

    public abstract void ProcesarOrden(OrdenCompra ordenCompra);
}

// Clase de Aprobador: Supervisor
public class Supervisor : Aprobador
{
    public override void ProcesarOrden(OrdenCompra ordenCompra)
    {
        if (ordenCompra.Monto <= 10000)
        {
            Console.WriteLine($"Orden de compra {ordenCompra.Id} aprobada por el Supervisor");
        }
        else if (_siguienteAprobador != null)
        {
            _siguienteAprobador.ProcesarOrden(ordenCompra);
        }
    }
}

// Clase de Aprobador: Gerente
public class Gerente : Aprobador
{
    public override void ProcesarOrden(OrdenCompra ordenCompra)
    {
        if (ordenCompra.Monto <= 50000)
        {
            Console.WriteLine($"Orden de compra {ordenCompra.Id} aprobada por el Gerente");
        }
        else if (_siguienteAprobador != null)
        {
            _siguienteAprobador.ProcesarOrden(ordenCompra);
        }
    }
}

// Clase de Aprobador: Director
public class Director : Aprobador
{
    public override void ProcesarOrden(OrdenCompra ordenCompra)
    {
        if (ordenCompra.Monto <= 100000)
        {
            Console.WriteLine($"Orden de compra {ordenCompra.Id} aprobada por el Director");
        }
        else
        {
            Console.WriteLine($"Orden de compra {ordenCompra.Id} rechazada por el Director");
        }
    }
}

// Ejemplo de uso
public class AppChainOfResponsability
{
    public static void Ejecutar()
    {
        Console.WriteLine("Patron Chain of Responsability");

        // Crear los aprobadores
        Aprobador supervisor = new Supervisor();
        Aprobador gerente = new Gerente();
        Aprobador director = new Director();

        // Configurar la cadena de aprobación
        supervisor.SetSiguienteAprobador(gerente);
        gerente.SetSiguienteAprobador(director);

        // Crear una orden de compra
        OrdenCompra ordenCompra = new OrdenCompra(1, "Proveedor 1")
        {
            Monto = 20000
        };

        // Procesar la orden de compra
        supervisor.ProcesarOrden(ordenCompra);
    }
}