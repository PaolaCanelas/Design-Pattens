namespace PatronesCreacionales.SimpleFactory;

public class DocumentoAcademicoFactory
{
    public static IDocumentoAcademico CrearDocumento(string tipoDocumento)
    {
        switch (tipoDocumento.ToLower())
        {
            case "constancia":
                return new ConstanciaEstudios();
            case "recomendacion":
                return new CartaRecomendacion();
            default:
                throw new ArgumentException($"Tipo de Documento '{tipoDocumento}' no reconocido");
        }
    }
}