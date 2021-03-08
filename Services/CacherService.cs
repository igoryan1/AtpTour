using ATP.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace ATP.Services
{
    public interface ICacherService
    {
        PlayerModel CacheTryGetValue(string playerId);
        void SetCacheValue(string playerId, PlayerModel bio);
    }

    public class CacherService : ICacherService
    {
        private IMemoryCache _cache;
        private static string _cacheKeyPrefix = "PlayerBio_";

        public CacherService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public PlayerModel CacheTryGetValue(string playerId)
        {
            PlayerModel cacheEntry;

            // Look for cache key e.g. PlayerBio_D643
            if (!_cache.TryGetValue($"{_cacheKeyPrefix}{playerId}", out cacheEntry))
            {
                return null;
            }

            return cacheEntry;
        }

        public void SetCacheValue(string playerId, PlayerModel bio)
        {
            var cacheKey = $"{_cacheKeyPrefix}{playerId}";

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for 30 min, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(cacheKey, bio, cacheEntryOptions);
        }
    }
}
