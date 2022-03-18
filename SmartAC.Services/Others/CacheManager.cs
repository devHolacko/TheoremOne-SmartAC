using Microsoft.Extensions.Caching.Memory;
using SmartAC.Models.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Others
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Add(string key, string value)
        {
            bool keyExists = _memoryCache.TryGetValue(key, out _);
            if (keyExists)
            {
                _memoryCache.Remove(key);
            }

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                       .SetAbsoluteExpiration(
                             TimeSpan.FromDays(7));

            _memoryCache.Set(key, value, options);
        }

        public string Get(string key)
        {
            _memoryCache.TryGetValue(key, out string value);
            return value;
        }

        public void Remove(string key)
        {
            string value = Get(key);
            if (!string.IsNullOrEmpty(value))
                _memoryCache.Remove(key);
        }
    }
}
