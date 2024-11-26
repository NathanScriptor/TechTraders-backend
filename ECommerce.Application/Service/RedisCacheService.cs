using ECommerce.Application.Interfaces.IServices;
using StackExchange.Redis;

namespace ECommerce.Application.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisCacheService(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public async Task<string?> GetCachedDataAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task SetCachedDataAsync(string key, string value, TimeSpan expiration)
        {
            await _db.StringSetAsync(key, value, expiration);
        }
    }
}
