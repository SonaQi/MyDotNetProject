using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.MemoryCache
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set(string key, object value, TimeSpan expireTimeSpan)
        {
            _cache.Set(key, value, expireTimeSpan);
        }

        public T Get<T>(string key) where T : class
        {
            return _cache.Get(key) as T;
        }

        public List<T> GetList<T>(string key) where T : class
        {
            return _cache.Get(key) as List<T>;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
