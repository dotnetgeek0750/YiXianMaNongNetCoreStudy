using Microsoft.Extensions.Caching.Redis;
using System;
using System.Text;

namespace ConsoleAppRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化RedisCache
            RedisCache redisCache = new RedisCache(new RedisCacheOptions()
            {
                Configuration = "127.0.0.1:6379",
                InstanceName = "testName",
            });
            //Set插入操作
            redisCache.Set("username", Encoding.UTF8.GetBytes("chh"), new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
            });
            //Get读取操作
            var info = Encoding.UTF8.GetString(redisCache.Get("username"));

            Console.ReadKey();
        }
    }
}
