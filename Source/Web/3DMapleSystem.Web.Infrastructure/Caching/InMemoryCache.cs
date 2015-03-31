using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _3DMapleSystem.Web.Infrastructure.Caching
{
    public class InMemoryCache : ICacheService
    {
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item);
            }
            return item;
        }

        public void Clear(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }
    }
}
