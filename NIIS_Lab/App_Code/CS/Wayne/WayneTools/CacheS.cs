using System;
using System.Web.Configuration;

namespace WayneTools
{
    public class CacheS
    {
        public static object GetCache(string CacheId)
        {
            object objCache = System.Web.HttpRuntime.Cache.Get(CacheId);

            if (WebConfigurationManager.AppSettings["EnableCache"] == null || Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableCache"]) == false )
            {
                objCache = null;
                System.Web.HttpRuntime.Cache.Remove(CacheId);
            }

            return objCache;
        }

        public static  void SetCache(string CacheId, object objCache)
        {
            if (WebConfigurationManager.AppSettings["CacheDurationSeconds"] != null)
            {
                SetCache(CacheId, objCache, Convert.ToInt32(WebConfigurationManager.AppSettings["CacheDurationSeconds"]));
            }
            else
            {
                SetCache(CacheId, objCache, 60);
            }
        }

        public static  void SetCache(string CacheId, object objCache, int cacheDurationSeconds)
        {
            if (objCache != null)
            {
                System.Web.HttpRuntime.Cache.Insert(CacheId
                                                   , objCache
                                                   , null
                                                   , System.Web.Caching.Cache.NoAbsoluteExpiration
                                                   , new TimeSpan(0, 0, cacheDurationSeconds)
                                                   , System.Web.Caching.CacheItemPriority.High
                                                   , null);
            }
        }
    }
}