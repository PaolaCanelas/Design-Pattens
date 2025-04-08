namespace PatronesCreacionales.AbstractFactoryDb;

public interface IDbConnection
{
    void Open();
    void Close();
    string ConnectionString { get; set; }
}

public interface IDbCommand
{
    void SetCommandText(string commandText);
    void ExecuteNonQuery();
    IDataReader ExecuteReader();
}

public interface IDataReader
{
    bool Read();
    string GetString(int columnIndex);
    void Close();
}


// Clases de productos concretos para SQL Server
public class SqlServerConnection : IDbConnection
{
    public void Open() => Console.WriteLine("SQL Server connection abierta");

    public void Close() => Console.WriteLine("SQL Server cerrado");

    public string ConnectionString { get; set; } = null!;
}

public class SqlServerCommand : IDbCommand
{
    private readonly SqlServerConnection _connection;
    private string _commandText;

    public SqlServerCommand(SqlServerConnection connection)
    {
        _connection = connection;
    }

    public void SetCommandText(string commandText)
    {
        _commandText = commandText;
    }

    public void ExecuteNonQuery()
    {
        Console.WriteLine("Ejecutando query de SQL");
    }

    public IDataReader ExecuteReader()
    {
        Console.WriteLine("Ejecutando reader");
        return new SqlServerReader();
    }
}

public class SqlServerReader : IDataReader
{
    public bool Read()
    {
        Console.WriteLine("Leyendo siguiente registro de SQL Server");
        return true;
    }

    public string GetString(int columnIndex) => $"Leyendo datos de la columna {columnIndex}";

    public void Close() => Console.WriteLine("Cerrando reader");
}

// Interfaz abstracta para Factory
public interface IDatabaseFactory
{
    IDbConnection CreateConnection();
    IDbCommand CreateCommand(IDbConnection connection);
    IDataReader CreateDataReader();
}

// Fabrica concreta
public class SqlServerDbFactory : IDatabaseFactory
{
    public IDbConnection CreateConnection() => new SqlServerConnection();

    public IDbCommand CreateCommand(IDbConnection connection)
        => new SqlServerCommand((SqlServerConnection)connection);

    public IDataReader CreateDataReader() => new SqlServerReader();
}

// Codigo Cliente

public class DatabaseClient
{
    private readonly IDatabaseFactory _databaseFactory;
    private IDbConnection _connection;
    private IDbCommand _command;
    private IDataReader _reader;

    public DatabaseClient(IDatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
    }

    public void ExecuteQuery(string query)
    {
        _connection = _databaseFactory.CreateConnection();
        _connection.ConnectionString = "Mi cadena de conexion";
        _connection.Open();

        _command = _databaseFactory.CreateCommand(_connection);
        _command.SetCommandText(query);

        _reader = _command.ExecuteReader();

        if (_reader.Read())
        {
            Console.WriteLine($"Data {_reader.GetString(0)}");
        }

        _reader.Close();
        _connection.Close();
    }
}

public class SistemaBaseDatos
{
    public static void Ejecutar()
    {
        IDatabaseFactory sqlServerFactory = new SqlServerDbFactory();
        DatabaseClient client = new DatabaseClient(sqlServerFactory);
        client.ExecuteQuery("SELECT * FROM CLIENTES");

        // Implementar mas productos derivados.
    }
}