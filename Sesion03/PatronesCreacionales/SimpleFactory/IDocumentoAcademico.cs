namespace PatronesCreacionales.SimpleFactory;

// Interfaz base para el documento academico
public interface IDocumentoAcademico
{
    void Generar(int idEstudiante);
    void Imprimir();
    string ObtenerNombreDocumento();
}