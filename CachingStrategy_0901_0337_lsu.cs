// 代码生成时间: 2025-09-01 03:37:43
// CachingStrategy.cs
// This class implements a basic caching strategy using ASP.NET caching mechanisms.
using System;
using System.Runtime.Caching;
using System.Web.Caching;

namespace CachingDemo
{
    public class CachingStrategy : ICache
    {
        private MemoryCache _cache;
        public CachingStrategy()
        {
            _cache = MemoryCache.Default;
        }

        // Retrieves an item from the cache with the specified key.
        public object Get(string key)
        {
            // Check if the cache contains the item before returning it.
            // This method will return null if the item is not found.
            return _cache.Get(key);
        }

        // Adds an item to the cache with the specified key and value.
        public void Set(string key, object value, CacheItemPolicy policy)
        {
            // Adds or updates an item in the cache with the specified policy.
            _cache.Add(key, value, policy);
        }

        // Removes an item from the cache with the specified key.
        public void Remove(string key)
        {
            // Removes the specified item from the cache.
            _cache.Remove(key);
        }

        // Clears all items from the cache.
        public void Clear()
        {
            // Clears all items from the cache.
            _cache.Dispose();
        }
    }

    // CacheItemPolicy configuration class for setting cache policy.
    public class CacheItemPolicyConfigurator
    {
        private CacheItemPolicy _policy;

        public CacheItemPolicyConfigurator()
        {
            _policy = new CacheItemPolicy();
        }

        // Sets the absolute expiration time for the cache item.
        public CacheItemPolicyConfigurator SetAbsoluteExpiration(DateTime absoluteExpiration)
        {
            _policy.AbsoluteExpiration = absoluteExpiration;
            return this;
        }

        // Sets the sliding expiration time for the cache item.
        public CacheItemPolicyConfigurator SetSlidingExpiration(TimeSpan slidingExpiration)
        {
            _policy.SlidingExpiration = slidingExpiration;
            return this;
        }

        // Builds and returns the CacheItemPolicy.
        public CacheItemPolicy Build()
        {
            return _policy;
        }
    }

    // Interface for cache operations to ensure the code is easily maintainable and extensible.
    public interface ICache
    {
        object Get(string key);
        void Set(string key, object value, CacheItemPolicy policy);
        void Remove(string key);
        void Clear();
    }
}
