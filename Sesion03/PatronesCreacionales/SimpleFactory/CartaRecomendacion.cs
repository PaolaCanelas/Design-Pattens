namespace PatronesCreacionales.SimpleFactory;

public class CartaRecomendacion : IDocumentoAcademico
{
    private string _contenido;
    private string _nombreEstudiante;
    private string _nombreProfesor = "Ing. Erick Velasco";

    public void Generar(int idEstudiante)
    {
        _nombreEstudiante = $"Estudiante {idEstudiante}";
        _contenido = $"A quien corresponda\n\nPor medio de la presente, recomiendo a {_nombreEstudiante}"+
                     $"Atentamente\n{_nombreProfesor}";
    }

    public void Imprimir()
    {
        Console.WriteLine($"Imprimiendo Carta de recomendación para {_nombreEstudiante}");
        Console.WriteLine(_contenido);
        Console.WriteLine("------Fin------");
    }

    public string ObtenerNombreDocumento()
    {
        return "Carta de Recomendación";
    }

}