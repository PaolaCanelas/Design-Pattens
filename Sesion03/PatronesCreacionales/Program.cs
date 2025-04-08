using PatronesCreacionales.AbstractFactory;
using PatronesCreacionales.AbstractFactoryDb;
using PatronesCreacionales.FactoryMethod;
using PatronesCreacionales.SimpleFactory;

Console.WriteLine("Simple Factory Pattern");

SistemaDocumentos.Ejecutar();

Console.WriteLine("Factory Method Pattern");

SistemaNotificaciones.Ejecutar();

Console.WriteLine("Abstract Factory");

SistemaCliente.Ejecutar();
SistemaBaseDatos.Ejecutar();