using System;
using System.Threading.Tasks;

namespace Domain.Compartilhado.Contracts;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? ttl = null);
}
