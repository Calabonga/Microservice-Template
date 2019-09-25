using System;
using Microsoft.Extensions.Caching.Memory;

namespace $safeprojectname$.Infrastructure.Services
{
    /// <summary>
    /// Cache service interface
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Get the entry from the cache
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <returns></returns>
        TEntry Get<TEntry>(object key);

        /// <summary>
        /// Sets entry cache for one minute sliding expiration
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheEntry"></param>
        void SetForMinute<TEntry>(object key, TEntry cacheEntry);

        /// <summary>
        /// Sets entry cache for 30 minutes sliding expiration
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheEntry"></param>
        void SetForThirtyMinutes<TEntry>(object key, TEntry cacheEntry);

        /// <summary>
        /// Sets entry cache for custom sliding expiration interval 
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheEntry"></param>
        /// <param name="slidingExpiration"></param>
        void SetWithSlidingExpiration<TEntry>(object key, TEntry cacheEntry, TimeSpan slidingExpiration);

        /// <summary>
        /// Returns already exist entry or first put it to the cache and then return entry
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="findIfNotFoundFunc"></param>
        /// <returns></returns>
        TEntry GetOrCreate<TKey, TEntry>(TKey key, Func<ICacheEntry, TEntry> findIfNotFoundFunc);
    }
}
