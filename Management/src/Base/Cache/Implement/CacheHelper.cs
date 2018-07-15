using System.Web;

namespace Eagles.Base.Cache.Implement
{
    public class CacheHelper:ICacheHelper
    {

        public void SetData<T>(string key, T data)
        {
            HttpContext.Current.Cache.Insert(key,data);
        }

        public T GetData<T>(string key) where T : class
        {
            var data = HttpContext.Current.Cache.Get(key);
            return data as T;
        }
    }
}
