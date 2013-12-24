using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Yotta.Caching
{
    public class BasicCacher: BaseCacher
    {
        private static ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;

        protected override void AddToMyCache(String CacheKeyName, Object CacheItem, CacherPriority Priority)
        {
            callback = new CacheEntryRemovedCallback(this.CacherItemRemovedCallback);
            policy = new CacheItemPolicy();
            policy.Priority = (Priority == CacherPriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00);
            policy.RemovedCallback = callback;
            cache.Set(CacheKeyName, CacheItem, policy);
        }

        protected override Object GetChaceItem(String CacheKeyName)
        {
            return cache[CacheKeyName] as Object;
        }

        protected override void RemoveCacheItem(String CacheKeyName)
        {
            if (cache.Contains(CacheKeyName))
            {
                cache.Remove(CacheKeyName);
            }
        }

        private void CacherItemRemovedCallback(CacheEntryRemovedArguments arguments) 
        { 
            // Log these values from arguments list 
            String strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), " | Key-Name: ", arguments.CacheItem.Key, " | Value-Object: ", arguments.CacheItem.Value.ToString()); 
        }

    }
}
