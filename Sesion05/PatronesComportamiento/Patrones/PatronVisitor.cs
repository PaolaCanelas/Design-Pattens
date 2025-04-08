using System;

namespace PatronesComportamiento.Patrones;

// 1. Definir la interfaz Visitor
public interface IVisitor
{

    void Visit(Book book);
    void Visit(Dvd dvd);

    void Visit(Fruit fruit);
}

// 2. Definir la interfaz Visitable
public interface IProduct
{

    void Accept(IVisitor visitor);
}

// 3. Implementacion concreta de los elementos
public class Book : IProduct
{
    public string Title { get; set; }
    public double Price { get; set; }

    public Book(string title, double price)
    {
        Title = title;
        Price = price;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Dvd : IProduct
{
    public string Title { get; set; }
    public double Price { get; set; }

    public Dvd(string title, double price)
    {
        Title = title;
        Price = price;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Fruit : IProduct
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Fruit(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// 4. Implementacion concreta del Visitor
public class PriceVisitor : IVisitor
{

    private double _discountRate = 0.1;

    public PriceVisitor(double discountRate)
    {
        _discountRate = discountRate;
    }

    public void Visit(Book book)
    {
        double discount = book.Price * _discountRate;
        Console.WriteLine($"Descuento de {discount} aplicado al libro {book.Title}. Precio final {book.Price - discount:C}");
    }

    public void Visit(Dvd dvd)
    {
        double discount = dvd.Price * _discountRate;
        Console.WriteLine($"Descuento de {discount} aplicado al DVD {dvd.Title}. Precio final {dvd.Price - discount:C}");
    }

    public void Visit(Fruit fruit)
    {
        double discount = fruit.Price * _discountRate;
        Console.WriteLine($"Descuento de {discount} aplicado a la fruta {fruit.Name}. Precio final {fruit.Price - discount:C}");
    }
}

// 5. Implementacion del cliente

public class AppVisitor
{
    public static void Ejecutar()
    {
        Console.WriteLine("Patron Visitor");

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");

        var book = new Book("El Quijote", 20);
        var dvd = new Dvd("El Padrino", 30);
        var fruit = new Fruit("Manzana", 2);

        var products = new List<IProduct> { book, dvd };

        var visitor = new PriceVisitor(0.1);

        foreach (var product in products)
        {
            product.Accept(visitor);
        }
    }
}
