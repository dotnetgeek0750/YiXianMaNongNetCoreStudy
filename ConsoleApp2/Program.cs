using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置MemoryCache最大容量限制
            //MaxCapacity();
            //缓存被动过期
            // PassiveExpired();
            //主动过期
            ActiveExpired();
            Console.Read();
        }


        /// <summary>
        /// 设置MemoryCache最大容量限制
        /// </summary>
        static void MaxCapacity()
        {
            MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 100, //设置MemoryCache最大限制100
            });
            for (int i = 0; i < 1000; i++)
            {
                memoryCache.Set<string>(i.ToString(), i.ToString(), new MemoryCacheEntryOptions
                {
                    Size = 5,
                });
                //打印当前memoryCache的总数，
                //因为被设置了最大限制100，所以此处不会超过100
                Console.WriteLine(memoryCache.Count);
            }
        }

        /// <summary>
        /// 缓存被动过期
        /// </summary>
        static void PassiveExpired()
        {
            MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

            //设置CacheOption 5s后过期
            var cacheOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(5),
            };
            //扩展函数：过期后do something
            cacheOption.RegisterPostEvictionCallback((key, value, reason, obj) =>
            {
                Console.WriteLine($"key：{key}过期啦！" + reason);
            });
            //Cache Set并设置cacheOption
            memoryCache.Set("key", "value", cacheOption);

            //过期触发函数RegisterPostEvictionCallback不会自己执行
            //需要Get的时候发现过期了才会执行
            while (true)
            {
                memoryCache.Get("key");
                Thread.Sleep(1);
            }
        }


        /// <summary>
        /// 主动过期
        /// 举例：设置10秒过期，可以让他提前过期
        /// </summary>
        static void ActiveExpired()
        {
            MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            //new取消的TokenSource
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            //设置CacheOption 5s后过期
            var cacheOption = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(5),
            };
            //扩展函数：过期后do something
            cacheOption.RegisterPostEvictionCallback((key, value, reason, obj) =>
            {
                Console.WriteLine($"key：{key}过期啦！" + reason);
            });
            cacheOption.AddExpirationToken(new CancellationChangeToken(tokenSource.Token));

            //Cache Set并设置cacheOption
            memoryCache.Set("key", "value", cacheOption);

            //设置3秒之后，让MemoryCache过期
            tokenSource.CancelAfter(1000 * 3);
        }
    }
}
