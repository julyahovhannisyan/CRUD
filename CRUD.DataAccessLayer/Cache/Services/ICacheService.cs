namespace CRUD.DataAccessLayer.Cache.Services;

public interface ICacheService
{
    Task<bool> ExistsAsync(string key);
    Task<TEntity> SetValueAsync<TEntity>(string key, TEntity value, TimeSpan? ts = null);
    Task<TEntity> GetValueAsync<TEntity>(string key) where TEntity : class;
    Task<IEnumerable<(string key, TEntity value)>> GetAllByPatternAsync<TEntity>(string pattern);
    Task RemoveAsync(string key);
}
