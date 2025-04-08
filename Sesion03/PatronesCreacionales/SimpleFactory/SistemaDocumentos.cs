namespace PatronesCreacionales.SimpleFactory;

public class SistemaDocumentos
{
    public static void Ejecutar()
    {
        Console.WriteLine("=== SISTEMA DE DOCUMENTOS ACADEMICOS ===");

        try
        {
            // El estudiante solicita una constancia desde el sistema
            int idEstudiante = 1345;
            IDocumentoAcademico constancia = DocumentoAcademicoFactory.CrearDocumento("constancia");
            constancia.Generar(idEstudiante);
            constancia.Imprimir();
            
            
            // El estudiante solicita una carta de recomendacion desde el sistema
            IDocumentoAcademico carta = DocumentoAcademicoFactory.CrearDocumento("recomendacion");
            carta.Generar(idEstudiante);
            carta.Imprimir();

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error {ex.Message}");
        }
    }
}