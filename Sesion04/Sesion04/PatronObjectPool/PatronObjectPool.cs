using System.Collections.Concurrent;

namespace Sesion04.PatronObjectPool;

public class HttpClientPool
{
    private readonly ConcurrentBag<HttpClient> _pool;
    private readonly int _maxSize;

    public HttpClientPool(int maxSize = 5)
    {
        _pool = new ConcurrentBag<HttpClient>();
        _maxSize = maxSize;
    }

    public HttpClient GetClient()
    {
        if (_pool.TryTake(out HttpClient? client))
        {
            return client;
        }

        return new HttpClient(); // Si no hay clientes en el pool, crea uno nuevo
    }

    public void ReturnClient(HttpClient client)
    {
        if (_pool.Count < _maxSize)
        {
            _pool.Add(client);
        }
        else
        {
            client.Dispose();
        }
    }
}