using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Yotta.Caching
{
    public enum CacherPriority
    {
        Default,
        NotRemovable
    }
    public delegate R Function<R>(dynamic list);
    public abstract class BaseCacher
    {
        protected abstract void AddToMyCache(String CacheKeyName, Object CacheItem, CacherPriority Priority);
        protected abstract Object GetChaceItem(String CacheKeyName);
        protected abstract void RemoveCacheItem(String CacheKeyName);
        public T Invoke<T>(Function<T> theFunction, dynamic list)
        {
            String key = theFunction.ToString() + new JavaScriptSerializer().Serialize(list);

            Object result = this.GetChaceItem(key);

            if (result == null)
            {
                result = theFunction(list);
                this.AddToMyCache(key, result, CacherPriority.Default);
            }

            return (T) Convert.ChangeType(result, typeof(T));
        }
    }
}
