namespace PatronesCreacionales.SimpleFactory;

public class ConstanciaEstudios : IDocumentoAcademico
{
    private string _contenido;
    private string _nombreEstudiante;

    public void Generar(int idEstudiante)
    {
        _nombreEstudiante = $"Estudiante {idEstudiante}";
        _contenido = $"Por medio de la presente se certifica que el {_nombreEstudiante} es un buen alumno";
    }

    public void Imprimir()
    {
        Console.WriteLine($"Imprimiendo constancia de estudios para {_nombreEstudiante}");
        Console.WriteLine(_contenido);
        Console.WriteLine("------Fin------");
    }

    public string ObtenerNombreDocumento()
    {
        return "Constancia de estudios";
    }
}