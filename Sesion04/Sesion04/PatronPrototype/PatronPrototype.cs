
namespace Sesion04.PatronPrototype;

public interface IDocumentoPrototype<T>
    where T : class
{
    T Clone(); // Metodo para clonar
}

public abstract class DocumentoBase
{
    public DateTime CreationDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? DocumentType { get; set; }

    public virtual void DisplayHeader()
    {
        Console.WriteLine($"--{DocumentType}--");
        Console.WriteLine($"Creado el dia--{CreationDate} por {CreatedBy ?? "Fulano"}--");
    }
}

public class OrdenCompra : DocumentoBase, IDocumentoPrototype<OrdenCompra>
{
    public int Id { get; set; }
    public string NombreVendedor { get; set; } = null!;

    public OrdenCompra()
    {
        Id = 50;
        NombreVendedor = string.Empty;
    }

    public OrdenCompra Clone()
    {
        // Copia superficial (shallow copy)

        //OrdenCompra clonado = (OrdenCompra)this.MemberwiseClone();

        // Copia profunda (Deep Copy)
        var clonado = new OrdenCompra()
        {
            Id = Id,
            NombreVendedor = NombreVendedor,
            CreatedBy = CreatedBy,
            CreationDate = CreationDate,
            DocumentType = "OrdenCompra"
        };

        return clonado;
    }

    public override void DisplayHeader()
    {
        base.DisplayHeader();

        Console.WriteLine($"Orden ID: {Id} | Vendedor {NombreVendedor}");

        Console.WriteLine(new string('-', 50));
    }
}

// Registro para gestionar las plantillas

public class RegistroDoumentos<T>
where T : DocumentoBase
{
    private Dictionary<string, IDocumentoPrototype<T>> _templates = new();

    public void RegisterTemplate(string key, IDocumentoPrototype<T> template)
    {
        _templates[key] = template;
    }

    public T? CreateDocument(string key)
    {
        if (_templates.TryGetValue(key, out var template))
        {
            // Aqui es donde se usa el patron de Prototype
            return template.Clone() as T;
        }

        return null;
    }
}

public class AppNegocio
{
    public static void Ejecutar()
    {
        var registro = new RegistroDoumentos<OrdenCompra>();

        var primerPlantilla = new OrdenCompra()
        {
            Id = 30,
            CreatedBy = "Erick Orlando",
            CreationDate = DateTime.Now
        };

        registro.RegisterTemplate("DocumentoStandard", primerPlantilla);

        primerPlantilla.DisplayHeader();

        Console.WriteLine("Creando una nueva OC");

        OrdenCompra? nueva = registro.CreateDocument("DocumentoStandard");

        if (nueva is not null)
        {
            nueva.NombreVendedor = "Fulano de tal";
            nueva.CreatedBy = "Juanito";
            nueva.DisplayHeader();
        }

        Console.WriteLine("La plantilla no tiene porque cambiar");
        primerPlantilla.DisplayHeader();
    }
}
