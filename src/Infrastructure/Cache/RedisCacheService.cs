using System.Text.Json;
using StackExchange.Redis;
using Domain.Compartilhado.Contracts;

namespace Radar.Infrastructure.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    public RedisCacheService(IConnectionMultiplexer mux)
    { _db = mux.GetDatabase(); }

    public async Task<T?> GetAsync<T>(string key)
    {
        var v = await _db.StringGetAsync(key);
        if (v.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(v!);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
    {
        var json = JsonSerializer.Serialize(value);
        return _db.StringSetAsync(key, json, ttl);
    }
}
