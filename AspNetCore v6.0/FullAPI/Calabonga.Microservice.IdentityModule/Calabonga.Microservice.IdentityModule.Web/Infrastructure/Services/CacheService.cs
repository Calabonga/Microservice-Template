using Calabonga.Microservices.Core.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Services
{
    /// <summary>
    /// Cache service 
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultSlidingExpiration = TimeSpan.FromSeconds(60);

        public CacheService(IMemoryCache cache) => _cache = cache;

        /// <inheritdoc />
        public TEntry Get<TEntry>(object key)
        {
            if (key == null)
            {
                throw new MicroserviceArgumentNullException(nameof(key));
            }

            return _cache.Get<TEntry>(key);
        }

        /// <inheritdoc />
        public void SetForMinute<TEntry>(object key, TEntry cacheEntry) => SetWithSlidingExpiration(key, cacheEntry, _defaultSlidingExpiration);

        /// <inheritdoc />
        public void SetForThirtyMinutes<TEntry>(object key, TEntry cacheEntry) => SetWithSlidingExpiration(key, cacheEntry, TimeSpan.FromMinutes(30));

        /// <summary>
        /// Default set mechanism
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheEntry"></param>
        /// <param name="slidingExpiration"></param>
        public void SetWithSlidingExpiration<TEntry>(object key, TEntry cacheEntry, TimeSpan slidingExpiration)
        {
            if (cacheEntry == null)
            {
                throw new MicroserviceArgumentNullException(nameof(cacheEntry));
            }

            if (key == null)
            {
                throw new MicroserviceArgumentNullException(nameof(key));
            }

            if (slidingExpiration.Ticks == 0)
            {
                slidingExpiration = _defaultSlidingExpiration;
            }

            var options = new MemoryCacheEntryOptions().SetSlidingExpiration(slidingExpiration);

            _cache.Set(key, cacheEntry, options);
        }

        /// <inheritdoc />
        public TEntry GetOrCreate<TKey, TEntry>(TKey key, Func<ICacheEntry, TEntry> findIfNotFoundFunc) => _cache.GetOrCreate(key, findIfNotFoundFunc);
    }
}