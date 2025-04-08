namespace PatronesCreacionales.FactoryMethod;

public interface INotificacion
{
    void Configurar(string destinatario, string asunto, string cuerpo);
    bool Enviar();
    string ObtenerEstado();
}

public class NotificacionEmail : INotificacion
{
    private string _destinatario;
    private string _asunto;
    private string _cuerpo;
    private string _estado;

    public void Configurar(string destinatario, string asunto, string cuerpo)
    {
        _destinatario = destinatario;
        _asunto = asunto;
        _cuerpo = cuerpo;
        _estado = "Configurado";
    }

    public bool Enviar()
    {
        Console.WriteLine($"Enviando correo electronico a {_destinatario}");
        Console.WriteLine($"Asunto: {_asunto}");
        Console.WriteLine($"Cuerpo: {_cuerpo}");

        // simulamos el envio exitoso
        _estado = "Enviado";
        return true;
    }

    public string ObtenerEstado()
    {
        return $"Notificacion por Email - Estado {_estado}";
    }
}

public class NotificacionSms : INotificacion
{
    private string _destinatario;
    private string _asunto;
    private string _cuerpo;
    private string _estado;

    public void Configurar(string destinatario, string asunto, string cuerpo)
    {
        _destinatario = destinatario;
        _asunto = asunto;

        _cuerpo = cuerpo.Length > 160 ? cuerpo.Substring(0, 157) + "..." : cuerpo;
        _estado = "Configurado";
    }

    public bool Enviar()
    {
        Console.WriteLine($"Enviado SMS al numero {_destinatario}");
        Console.WriteLine($"Mensaje: {_cuerpo}");

        _estado = "Enviado";
        return true;
    }

    public string ObtenerEstado()
    {
        return $"Notificación por SMS - Estado {_estado}";
    }
}

/// <summary>
/// Creator: clase abstracta con Factory Method
/// </summary>
public abstract class ServicioNotificacion
{
    protected abstract INotificacion CrearNotificacion();

    // Logica compartida para todas las notificaciones
    public bool EnviarNotificacion(string destinatario, string asunto, string cuerpo)
    {
        // Aqui esta la magia del Factory Method: utilizamos el producto sin conocer su clase concreta
        INotificacion notificacion = CrearNotificacion();

        Console.WriteLine($"[LOG] Preparando notificacion para {destinatario}");

        notificacion.Configurar(destinatario, asunto, cuerpo);

        var resultado = notificacion.Enviar();

        Console.WriteLine($"[LOG] Resultado: {notificacion.ObtenerEstado()}");

        return resultado;
    }

}

/// <summary>
/// Concrete Creator de Email
/// </summary>
public class ServicioNotificacionEmail : ServicioNotificacion
{
    protected override INotificacion CrearNotificacion()
    {
        return new NotificacionEmail();
    }
}

/// <summary>
/// Concrete creator de SMS
/// </summary>
public class ServicioNotifacionSms : ServicioNotificacion
{
    protected override INotificacion CrearNotificacion()
    {
        return new NotificacionSms();
    }
}

public class SistemaNotificaciones
{
    public static void Ejecutar()
    {
        Console.WriteLine("=== SISTEMA DE NOTIFICACIONES ACADEMICAS ===");

        // Datos del estudiante
        string email = "estudiante@universidad.edu";
        string telefono = "+5198433455";
        string dispositivo = "ANDROID-1234T5";

        // Contenido de la notificacion
        string asunto = "Inscripcion exitosa";
        string mensaje = @"Estimado estudiante, le informamos que su inscripción al semetres 2025-A
                        ha sido procesada correctamente. Puede consultar su horario en la web.";

        // 1. Enviamos por email:
        ServicioNotificacion servicioEmail = new ServicioNotificacionEmail();
        Console.WriteLine("Enviando notificacion por email");
        servicioEmail.EnviarNotificacion(email, asunto, mensaje);

        Console.WriteLine();

        // 2. Enviamos por SMS:
        ServicioNotificacion servicioSms = new ServicioNotifacionSms();
        servicioSms.EnviarNotificacion(telefono, asunto, mensaje);

        Console.WriteLine();

    }
}