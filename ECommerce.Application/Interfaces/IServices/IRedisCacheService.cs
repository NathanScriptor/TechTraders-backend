namespace ECommerce.Application.Interfaces.IServices
{
    public interface IRedisCacheService
    {
        Task<string?> GetCachedDataAsync(string key);
        Task SetCachedDataAsync(string key, string value, TimeSpan expiration);
    }
}
