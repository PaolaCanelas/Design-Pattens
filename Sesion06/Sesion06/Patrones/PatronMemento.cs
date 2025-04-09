namespace Sesion06.Patrones;

// Clase memento - Almacena el estado
public class EditorMemmento
{
    private readonly string _content;

    public EditorMemmento(string content)
    {
        _content = content;
    }

    public string GetContent()
    {
        return _content;
    }
}

public class TextEditor
{
    private string _content;

    public string Content
    {
        get => _content;
        set => _content = value;
    }

    public EditorMemmento CreateMemento()
    {
        return new EditorMemmento(_content);
    }

    public void Restore(EditorMemmento memento)
    {
        _content = memento.GetContent();
    }
}

public class History
{
    private Stack<EditorMemmento> _mementos = new();
    public void AddMemento(EditorMemmento memento)
    {
        _mementos.Push(memento);
    }

    public EditorMemmento? Pop()
    {
        return _mementos.Count > 0 ? _mementos.Pop() : null;
    }
}

public class AppMemento
{
    public static void Ejecutar()
    {
        TextEditor editor = new TextEditor();
        History history = new History();

        // primer cambio
        editor.Content = "A";
        history.AddMemento(editor.CreateMemento());

        // segundo cambio
        editor.Content = "B";
        history.AddMemento(editor.CreateMemento());

        // tercer cambio
        editor.Content = "C";

        // Restaurar al segundo cambio
        var previo = history.Pop();
        if (previo is not null)
        {
            editor.Restore(previo);
            Console.WriteLine($"Estado restaurado: {editor.Content}");
        }
    }
}