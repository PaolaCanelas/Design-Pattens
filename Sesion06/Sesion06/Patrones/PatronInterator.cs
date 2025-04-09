using System.Collections;

namespace Sesion06.Patrones;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book()
    {

    }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

// Coleccion personalizada
public class BookCollection : IEnumerable<Book>
{
    private List<Book> _books = new();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return new BookIterator(this._books);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class BookIterator : IEnumerator<Book>
{
    private List<Book> _books;
    private int _position = -1;

    public BookIterator(List<Book> books)
    {
        _books = books;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public bool MoveNext()
    {
        _position++;
        return _position < _books.Count;
    }

    public void Reset()
    {
        _position = -1;
    }

    public Book Current => _books[_position];

    object IEnumerator.Current => Current;
}

// Uso con yield
public class SimpleBookCollection : IEnumerable<Book>
{
    private List<Book> _books = new();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public IEnumerator<Book> GetEnumerator()
    {
        foreach (var book in _books)
        {
            Console.WriteLine("uso de yield");
            yield return book;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class AppIterator
{
    public static void Ejecutar()
    {
        BookCollection books = new();
        books.AddBook(new Book("El Quijote", "Cervantes"));
        books.AddBook(new Book("La Iliada", "Homero"));
        books.AddBook(new Book("La Odisea", "Homero"));

        PrintBooks(books);

        SimpleBookCollection simpleBook = new SimpleBookCollection();
        simpleBook.AddBook(new Book("El Hobbit", "J.R.R. Tolkien"));
        simpleBook.AddBook(new Book("El Señor de los anilllos", "J.R.R. Tolkien"));
        simpleBook.AddBook(new Book("Harry Potter", "J.K. Rowling"));

        PrintBooks(simpleBook);
    }

    private static void PrintBooks(SimpleBookCollection books)
    {
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} by {book.Author}");
        }
    }

    private static void PrintBooks(BookCollection books)
    {
        foreach (var book in books) // Usa implicitamente el iterador
        {
            Console.WriteLine($"{book.Title} escrito por {book.Author}");
            Thread.Sleep(2000);
        }
    }
}