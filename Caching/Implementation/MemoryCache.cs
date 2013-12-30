using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yotta.Caching
{
    public class MemoryCache : BaseCacher
    {
        private static Dictionary<string, object> cache;
        private static object sync;

        static MemoryCache()
        {
            cache = new Dictionary<string, object>();
            sync = new object();
        }

        protected override void AddToMyCache(string CacheKeyName, object CacheItem, CacherPriority Priority)
        {
            lock (sync)
            {
                if (cache.ContainsKey(CacheKeyName))
                    return;

                lock (sync)
                {
                    cache.Add(CacheKeyName, CacheItem);
                }
            }
        }

        protected override object GetChaceItem(string CacheKeyName)
        {
            lock (sync)
            {
                if (cache.ContainsKey(CacheKeyName) == false)
                {
                    return null;
                }

                lock (sync)
                {
                    return cache[CacheKeyName];
                }
            }
        }

        protected override void RemoveCacheItem(string CacheKeyName)
        {
            lock (sync)
            {
                if (cache.ContainsKey(CacheKeyName) == false)
                    return;

                lock (sync)
                {
                    cache.Remove(CacheKeyName);
                }
            }
        }
    }
}
