using Newtonsoft.Json;
using StackExchange.Redis;

namespace CRUD.DataAccessLayer.Cache.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _cache;
    private readonly IServer _server;

    public CacheService(IDatabase cache, IServer server)
    {
        _cache = cache;
        _server = server;
    }

    public async Task<bool> ExistsAsync(string key) => await _cache.KeyExistsAsync(key);

    public async Task<TEntity> SetValueAsync<TEntity>(string key, TEntity value, TimeSpan? ts = null)
    {
        var valueJSON = JsonConvert.SerializeObject(value);

        await _cache.StringSetAsync(key, valueJSON, ts);

        return value;
    }

    public async Task<TEntity> GetValueAsync<TEntity>(string key) where TEntity : class
    {
        var result = await _cache.StringGetAsync(key);

        if (!result.HasValue)
            return null;

        return JsonConvert.DeserializeObject<TEntity>(result);
    }

    public async Task<IEnumerable<(string key, TEntity value)>> GetAllByPatternAsync<TEntity>(string pattern)
    {
        var result = new List<(string key, TEntity value)>();

        foreach (var key in _server.Keys())
        {
            var value = await _cache.StringGetAsync(key);

            if(value.HasValue)
                result.Add((key.ToString(), JsonConvert.DeserializeObject<TEntity>(value)));
        }

        return result;
    }

    public async Task RemoveAsync(string key) => await _cache.KeyDeleteAsync(key);
}
