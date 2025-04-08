namespace PatronesCreacionales.AbstractFactory;

// Interfaz abstracta para los productos
public interface IButton
{
    void Paint();
}

public interface ITextBox
{
    void DisplayText();
}

// Clases de productos concretos (estilo Windows)
public class WindowsButton : IButton
{
    public void Paint() => Console.WriteLine("Pintando boton estilo windows");
}

public class WindowsTextBox : ITextBox
{
    public void DisplayText() => Console.WriteLine("Textbox de estilo windows");
}

// Clases de productos concretos (estilo MacOs)

public class MacButton : IButton
{
    public void Paint() => Console.WriteLine("Pintando boton estilo MacOS");
}

public class MacTextBox : ITextBox
{
    public void DisplayText() => Console.WriteLine("Mostrando textbox estilo MacOS");
}

// Interfaz abstracta para la fabrica
public interface IGuiFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
}

// Fabrica concreta

public class WindowsGuiFactory : IGuiFactory
{
    public IButton CreateButton() => new WindowsButton();

    public ITextBox CreateTextBox() => new WindowsTextBox();
}

public class MacOsGuiFactory : IGuiFactory
{
    public IButton CreateButton() => new MacButton();

    public ITextBox CreateTextBox() => new MacTextBox();
}

// Codigo Cliente
public class UiController
{
    private readonly IGuiFactory _guiFactory;
    private readonly IButton _button;
    private readonly ITextBox _tetBox;

    public UiController(IGuiFactory guiFactory)
    {
        _guiFactory = guiFactory;
        _button = guiFactory.CreateButton();
        _tetBox = guiFactory.CreateTextBox();
    }

    public void Display()
    {
        _button.Paint();
        _tetBox.DisplayText();
    }
}

public class SistemaCliente
{
    public static void Ejecutar()
    {
        // Crear una interfaz de usuario estilo Windows
        IGuiFactory winFactory = new WindowsGuiFactory();
        UiController windowsUi = new UiController(winFactory);
        windowsUi.Display();

        Console.WriteLine();

        // Crear una interfaz de usuario con estilo MacOs
        IGuiFactory maFactory = new MacOsGuiFactory();
        UiController macUI = new UiController(maFactory);
        macUI.Display();
    }
}