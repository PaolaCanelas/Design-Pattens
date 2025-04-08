using System;
namespace PatronesComportamiento.Patrones;

/*
Escenario: Sistema de almacen, donde se tiene una orden de compra la cual maneja distintos estados, cada comando
debe representar un estado de la orden de compra.
*/

// Enumeracion
public enum EstadoOrdenCompra
{
    Pendiente,
    Aprobada,
    Rechazada,
    Cancelada,
    Completada
}

// Clase simple para representar un item en la orden de compra
public class ItemOrdenCompra
{
    public string Nombre { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public override string ToString()
    {
        return $"Item: {Nombre}, Precio: {Precio} | Cantidad: {Cantidad}";
    }
}

// El Receiver: Clase que representa la orden de compra
public class OrdenCompra
{
    public int Id { get; set; }
    public string Proveedor { get; set; } = null!;
    public EstadoOrdenCompra Estado { get; set; }
    public List<ItemOrdenCompra> Items { get; set; } = new();
    public decimal Monto { get; internal set; }

    private EstadoOrdenCompra _estadoAnterior;

    public OrdenCompra(int id, string proveedor)
    {
        Id = id;
        Proveedor = proveedor;
        Estado = EstadoOrdenCompra.Pendiente;
        Console.WriteLine($"Orden de compra creada: {Id} | Proveedor: {Proveedor}");
    }

    public void AgregarItem(ItemOrdenCompra item)
    {
        if (Estado == EstadoOrdenCompra.Pendiente)
        {
            Items.Add(item);
            Console.WriteLine($"Item agregado a la orden de compra: {item}");
        }
        else
        {
            Console.WriteLine("No se pueden agregar items a una orden que no está pendiente");
        }

    }

    public void QuitarItem(ItemOrdenCompra item)
    {
        if (Estado == EstadoOrdenCompra.Pendiente)
        {
            Items.Remove(item);
            Console.WriteLine($"Item eliminado de la orden de compra: {item}");
        }
        else
        {
            Console.WriteLine("No se pueden quitar items de una orden que no está pendiente");
        }
    }

    public void Aprobar()
    {
        if (Estado == EstadoOrdenCompra.Pendiente)
        {
            GuardarEstadoAnterior();
            Estado = EstadoOrdenCompra.Aprobada;
            Console.WriteLine($"Orden de compra #{Id} aprobada");
        }
        else
        {
            Console.WriteLine("No se puede aprobar una orden que no está pendiente");
        }
    }

    public void Cancelar()
    {
        if (Estado != EstadoOrdenCompra.Completada && Estado != EstadoOrdenCompra.Cancelada)
        {
            GuardarEstadoAnterior();
            Estado = EstadoOrdenCompra.Cancelada;
            Console.WriteLine($"Orden de compra #{Id} cancelada");
        }
        else
        {
            Console.WriteLine("No se puede cancelar una orden que ya está completada o cancelada");
        }
    }

    public void RevertirEstado()
    {
        if (_estadoAnterior != EstadoOrdenCompra.Pendiente)
        {
            Estado = _estadoAnterior;
            Console.WriteLine($"Estado de la orden de compra #{Id} revertido a {_estadoAnterior}");
        }
        else
        {
            Console.WriteLine("No se puede revertir el estado de una orden que está pendiente");
        }
    }

    private void GuardarEstadoAnterior()
    {
        _estadoAnterior = Estado;
    }

    public override string ToString()
    {
        return $"Orden de compra #{Id} | Proveedor: {Proveedor} | Estado: {Estado} - Items: {Items.Count}";
    }
}

// Comando abstracto
public interface IOrdenCompraCommand
{
    void Execute();
    void Undo(); // Esto es Opcional
}

// Comando para Aprobar una OC
public class AprobarOrdenCompraCommand : IOrdenCompraCommand
{
    private readonly OrdenCompra _ordenCompra;

    public AprobarOrdenCompraCommand(OrdenCompra ordenCompra)
    {
        _ordenCompra = ordenCompra;
    }

    public void Execute()
    {
        Console.WriteLine($"Aprobando orden de compra #{_ordenCompra.Id}");
        _ordenCompra.Aprobar();
    }

    public void Undo()
    {
        Console.WriteLine($"Deshaciendo aprobación de orden de compra #{_ordenCompra.Id}");
        _ordenCompra.RevertirEstado();
    }
}

// Comando para Cancelar una OC
public class CancelarOrdenCompraCommand : IOrdenCompraCommand
{
    private readonly OrdenCompra _ordenCompra;

    public CancelarOrdenCompraCommand(OrdenCompra ordenCompra)
    {
        _ordenCompra = ordenCompra;
    }

    public void Execute()
    {
        Console.WriteLine($"Cancelando orden de compra #{_ordenCompra.Id}");
        _ordenCompra.Cancelar();
    }

    public void Undo()
    {
        Console.WriteLine($"Deshaciendo cancelación de orden de compra #{_ordenCompra.Id}");
        _ordenCompra.RevertirEstado();
    }
}

// Comando para agregar un item a la orden
public class AgregarItemOrdenCompraCommand : IOrdenCompraCommand
{
    private readonly OrdenCompra _ordenCompra;
    private readonly ItemOrdenCompra _item;

    public AgregarItemOrdenCompraCommand(OrdenCompra ordenCompra, ItemOrdenCompra item)
    {
        _ordenCompra = ordenCompra;
        _item = item;
    }

    public void Execute()
    {
        Console.WriteLine($"Agregando item a la orden de compra #{_ordenCompra.Id}");
        _ordenCompra.AgregarItem(_item);
    }

    public void Undo()
    {
        Console.WriteLine($"Deshaciendo agregado de item a la orden de compra #{_ordenCompra.Id}");
        _ordenCompra.QuitarItem(_item);
    }
}

// Comando para quitar un item de la orden
public class QuitarItemOrdenCompraCommand : IOrdenCompraCommand
{
    private readonly OrdenCompra _ordenCompra;
    private readonly ItemOrdenCompra _item;

    public QuitarItemOrdenCompraCommand(OrdenCompra ordenCompra, ItemOrdenCompra item)
    {
        _ordenCompra = ordenCompra;
        _item = item;
    }

    public void Execute()
    {
        Console.WriteLine($"Quitando item de la orden de compra #{_ordenCompra.Id}");
        _ordenCompra.QuitarItem(_item);
    }

    public void Undo()
    {
        Console.WriteLine($"Deshaciendo eliminación de item de la orden de compra #{_ordenCompra.Id}");
        _ordenCompra.AgregarItem(_item);
    }
}

// Invoker
public class Invoker
{
    private readonly Stack<IOrdenCompraCommand> _commands = new();

    public void ExecuteCommand(IOrdenCompraCommand command)
    {
        _commands.Push(command);
        command.Execute();
    }

    public void UndoCommand()
    {
        if (_commands.Count > 0)
        {
            var command = _commands.Pop();
            command.Undo();
        }
        else
        {
            Console.WriteLine("No hay comandos para deshacer");
        }
    }
}

public class PatronCommandMejorado
{
    public static void Ejecutar()
    {
        Console.WriteLine("Patron Command Mejorado");

        // Crear una orden de compra
        var ordenCompra = new OrdenCompra(1, "Proveedor 1");

        // Crear items
        var item1 = new ItemOrdenCompra { Nombre = "Item 1", Cantidad = 2, Precio = 10 };
        var item2 = new ItemOrdenCompra { Nombre = "Item 2", Cantidad = 3, Precio = 20 };

        // Crear comandos
        var agregarItem1 = new AgregarItemOrdenCompraCommand(ordenCompra, item1);
        var agregarItem2 = new AgregarItemOrdenCompraCommand(ordenCompra, item2);
        var aprobarOrden = new AprobarOrdenCompraCommand(ordenCompra);
        var cancelarOrden = new CancelarOrdenCompraCommand(ordenCompra);

        // Invocador
        var invoker = new Invoker();

        // Ejecutar comandos
        invoker.ExecuteCommand(agregarItem1);
        invoker.ExecuteCommand(agregarItem2);
        invoker.ExecuteCommand(aprobarOrden);
        invoker.UndoCommand();
        invoker.UndoCommand();
        invoker.UndoCommand();
        invoker.UndoCommand();
    }
}