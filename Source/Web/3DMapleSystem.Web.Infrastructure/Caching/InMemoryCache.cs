using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _3DMapleSystem.Web.Infrastructure.Caching
{
    public class InMemoryCache : ICacheService
    {
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            var cache = MemoryCache.Default;
            T item = cache[cacheID] as T;

            if (item == null)
            {
                item = getItemCallback();
                cache.Set(cacheID, item, new DateTimeOffset(DateTime.Now.AddMinutes(2)));
            }
            return item;
        }

        public void Clear(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }
    }
}
