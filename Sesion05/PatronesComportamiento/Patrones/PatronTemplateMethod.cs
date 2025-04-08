namespace PatronesComportamiento.PatronesComportamiento.Patrones;

/* 
Escenario: Sistema para procesar datos de diferentes fuentes, pero existe un proceso general para eso.
*/

public abstract class DataProcessor
{
    public void ProcessData()
    {
        LoadData();
        ParseData();
        AnalyzeData();
        ReportData();
    }

    // Metodos que deben ser implementados por las subclases
    protected abstract void LoadData();
    protected abstract void ParseData();
    protected abstract void AnalyzeData();
    protected abstract void ReportData();
}

// Implementacion concreta para procesar archivos CSV
public class CsvDataProcessor : DataProcessor
{
    protected override void LoadData()
    {
        Console.WriteLine("Cargando datos desde archivo CSV");
    }

    protected override void ParseData()
    {
        Console.WriteLine("Parseando datos CSV");
    }

    protected override void AnalyzeData()
    {
        Console.WriteLine("Analizando datos CSV");
    }

    protected override void ReportData()
    {
        Console.WriteLine("Generando reporte de datos CSV");
    }
}

// Implementacion concreta para procesar archivos XML
public class XmlDataProcessor : DataProcessor
{
    protected override void LoadData()
    {
        Console.WriteLine("Cargando datos desde archivo XML");
    }

    protected override void ParseData()
    {
        Console.WriteLine("Parseando datos XML");
    }

    protected override void AnalyzeData()
    {
        Console.WriteLine("Analizando datos XML");
    }

    protected override void ReportData()
    {
        Console.WriteLine("Generando reporte de datos XML");
    }
}

public class AppTemplateMethod
{
    public static void Ejecutar()
    {
        Console.WriteLine("Patron Template Method");

        DataProcessor processor = new CsvDataProcessor();
        processor.ProcessData();

        processor = new XmlDataProcessor();
        processor.ProcessData();
    }
}