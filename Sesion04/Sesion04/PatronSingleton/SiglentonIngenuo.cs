using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Sesion04.PatronSingleton;
public class SiglentonIngenuo
{
    private static int NumeroCero;
    private static SiglentonIngenuo _instance;

    private SiglentonIngenuo() { }
    public static SiglentonIngenuo GetInstance()
    {
        if (_instance is null)
        {
           _instance = new SiglentonIngenuo();
        }
        return _instance;
    }

    public void ImprimeMensaje()
    {
        Console.WriteLine("Hola soy la instancia ingenua");
    }

}

public sealed class ThreadSafeSingleton
{
     private static ThreadSafeSingleton _instance;
    private static readonly object _padlock = new object();

    private ThreadSafeSingleton() { }

    public static ThreadSafeSingleton GetInstance()
    {
        if (_instance is null)
        {
            lock (_padlock)
            {
                _instance = new ThreadSafeSingleton();
            }
        }

        return _instance;
    }


    public void ImprimeMensaje()
    {
        Console.WriteLine("Hola soy instancia segura para hilos");
    }
}

public sealed class EagerSingleton
{
    private static readonly EagerSingleton _instance = new EagerSingleton();

    private EagerSingleton()
    {

    }

    public static EagerSingleton Instance => _instance;

    public void ImprimeMensaje()
    {
        Console.WriteLine("Hola soy la instancia con carga temprana de inicial");
    }
}

public sealed class SingletonLazy
{
    private static readonly Lazy<SingletonLazy> _instance = new(() => new SingletonLazy());

    private SingletonLazy()
    {
        Console.WriteLine($"Instancia creada {nameof(SingletonLazy)}");
    }

    public static SingletonLazy Instance => _instance.Value;

    public void ImprimeMensaje()
    {
        Console.WriteLine("Hola soy la instancia con carga diferida (Lazy)");
    }
}


public sealed class SingletonDoubleCheck
{
    private static volatile SingletonDoubleCheck _instance;
    private static readonly object _padLock = new();

    private SingletonDoubleCheck()
    {
        Console.WriteLine("Instancia creada a la antigua");
    }

    public static SingletonDoubleCheck GetInstance()
    {
        if (_instance is null)
        {
            lock (_padLock) 
            {
                if (_instance == null)
                {
                    _instance = new SingletonDoubleCheck();
                }
            }
        }

        return _instance;
    }

    public void ImprimeMensaje()
    {
        Console.WriteLine("Hola soy la instancia segura para hilos pero a la antigua sin lazy");
    }
}


