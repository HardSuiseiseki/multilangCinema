using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.Managers;

namespace Cinema.Extensions
{
    public static class CacheManagerExtensions
    {
        public static TResult CacheResult<TResult>(this ICacheManager cacheManager, Func<TResult> dataGetter,
            string cacheKey, TimeSpan? expirationSpan = null) where TResult : class
        {
            var resultModel = cacheManager.Get<TResult>(cacheKey);
            if (resultModel == null)
            {
                resultModel = dataGetter();
                if (resultModel != null)
                {
                    if (expirationSpan.HasValue)
                    {
                        cacheManager.Insert(resultModel, cacheKey, expirationSpan.Value);
                    }
                    else
                    {
                        cacheManager.Insert(resultModel, cacheKey);
                    }
                }
            }
            return resultModel;
        }

        public static IEnumerable<TResult> CacheResult<TResult>(this ICacheManager cacheManager, Func<IEnumerable<TResult>> dataGetter,
            string cacheKey, TimeSpan? expirationSpan = null) where TResult : class
        {
            var resultModel = cacheManager.Get<IEnumerable<TResult>>(cacheKey);
            if (resultModel == null)
            {
                resultModel = dataGetter();
                if (resultModel.Any())
                {
                    if (expirationSpan.HasValue)
                    {
                        cacheManager.Insert(resultModel, cacheKey, expirationSpan.Value);
                    }
                    else
                    {
                        cacheManager.Insert(resultModel, cacheKey);
                    }
                }
            }
            return resultModel;
        }

        public static TResult CacheResult<TResult>(this ICacheManager cacheManager, Func<TResult> dataGetter,
            string cacheKey, DateTime expirationTime) where TResult : class
        {
            var resultModel = cacheManager.Get<TResult>(cacheKey);
            if (resultModel == null)
            {
                resultModel = dataGetter();
                if (resultModel != null)
                {
                    cacheManager.Insert(resultModel, cacheKey, expirationTime);
                }
            }
            return resultModel;
        }

        public static IEnumerable<TResult> CacheResult<TResult>(this ICacheManager cacheManager, Func<IEnumerable<TResult>> dataGetter,
            string cacheKey, DateTime? expirationTime = null) where TResult : class
        {
            var resultModel = cacheManager.Get<IEnumerable<TResult>>(cacheKey);
            if (resultModel == null)
            {
                resultModel = dataGetter();
                if (resultModel.Any())
                {
                    if (expirationTime.HasValue)
                    {
                        cacheManager.Insert(resultModel, cacheKey, expirationTime.Value);
                    }
                    else
                    {
                        cacheManager.Insert(resultModel, cacheKey);
                    }
                }
            }
            return resultModel;
        }
    }
}