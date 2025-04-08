namespace Sesion04.PatronBuilder
{
    public class ServiceConnection
    {
        public string ServiceUrl { get; set; } = null!;
        public TimeSpan Timeout { get; set; }
        public int RetryAttempts { get; set; }
        public AuthenticationCredentials? Credentials { get; set; }
        public ProxySettings? Proxy { get; set; }
        public bool UseSsl { get; set; }


        internal ServiceConnection(string serviceUrl, TimeSpan timeout, int retryAttempts,
            AuthenticationCredentials? authenticationCredentials, ProxySettings? proxySettings, bool useSsl)
        {
            ServiceUrl = serviceUrl;
            Timeout = timeout;
            RetryAttempts = retryAttempts;
            Credentials = authenticationCredentials;
            Proxy = proxySettings;
            UseSsl = useSsl;
        }

        public void Connect()
        {
            Console.WriteLine($"Conectando a {ServiceUrl}");
            Console.WriteLine($"Timeout {Timeout}");
            Console.WriteLine($"Nro. de Intentos {RetryAttempts}");
            if (Credentials != null) Console.WriteLine($"Auth: {Credentials.Type}");
            if (Proxy != null) Console.WriteLine($"Proxy: {Proxy.Host}:{Proxy.Port}");
            if (UseSsl) Console.WriteLine($"Utilizando conexion SSL");

            Console.WriteLine("Conexion exitosa");
        }
    }

    public class ProxySettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
    }

    public class AuthenticationCredentials
    {
        public virtual string Type { get; set; } = null!;
    }

    public class BasicAuthCredentials : AuthenticationCredentials
    {
        public override string Type => "Basic";
        public string Username;
        public string Password;
    }

    public class TokenCredentials : AuthenticationCredentials
    {
        public override string Type => "Token";
        public string Token; /*...*/
    }

    // El Builder
    public class ServiceConnectionBuilder
    {
        private readonly string _serviceUrl; // Obligatorio, se pasa al constructor del builder
        private TimeSpan _timeout = TimeSpan.FromSeconds(30); // Valor por defecto
        private int _retryAttempts = 3; // Valor por defecto
        private AuthenticationCredentials? _credentials;
        private ProxySettings? _proxy;
        private bool _useSslCertificate = false; // Valor por defecto

        // El constructor del builder toma los parámetros obligatorios
        public ServiceConnectionBuilder(string serviceUrl)
        {
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Service URL is required.", nameof(serviceUrl));
            _serviceUrl = serviceUrl;
        }

        // Métodos fluidos para configurar las partes opcionales
        public ServiceConnectionBuilder WithTimeout(TimeSpan timeout)
        {
            _timeout = timeout;
            return this; // Permite encadenar llamadas (fluent interface)
        }

        public ServiceConnectionBuilder WithRetries(int attempts)
        {
            _retryAttempts = attempts > 0 ? attempts : 0;
            return this;
        }

        public ServiceConnectionBuilder WithBasicAuthentication(string username, string password)
        {
            _credentials = new BasicAuthCredentials { Username = username, Password = password };
            return this;
        }

        public ServiceConnectionBuilder WithTokenAuthentication(string token)
        {
            _credentials = new TokenCredentials { Token = token };
            return this;
        }

        public ServiceConnectionBuilder WithProxy(string host, int port)
        {
            _proxy = new ProxySettings { Host = host, Port = port };
            return this;
        }

        public ServiceConnectionBuilder UseSsl()
        {
            _useSslCertificate = true;
            return this;
        }

        // El método que finalmente construye el objeto
        public ServiceConnection Build()
        {
            // Aquí podrías añadir validaciones más complejas si fuera necesario
            // por ejemplo, si se requiere SSL si se usan ciertas credenciales.

            return new ServiceConnection(
                _serviceUrl,
                _timeout,
                _retryAttempts,
                _credentials,
                _proxy,
                _useSslCertificate
            );

            //ejemplo de como usarlo
            var connection = new ServiceConnectionBuilder("https://api.ejemplo.com")
                    .WithTimeout(TimeSpan.FromSeconds(10))
                    .WithRetries(5)
                    .WithTokenAuthentication("abc123")
                    .WithProxy("proxy.ejemplo.com", 8080)
                    .UseSsl()
                    .Build();

            connection.Connect();

        }
    }
}