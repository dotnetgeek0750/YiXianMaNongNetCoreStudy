using Microsoft.Extensions.Caching.MongoDB;
using System;
using System.Text;

using Microsoft.Extensions.Caching.Distributed;

namespace ConsoleAppMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDBCache mongoDBCache = new MongoDBCache(new MongoDBCacheOptions
            {
                ConnectionString = "mongodb//127.0.0.1:27017",
                DatabaseName = "mydb",
                CollectionName = "mytest",
            });
            mongoDBCache.Set("username", Encoding.UTF8.GetBytes("chh"), new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
            });
            var info = Encoding.UTF8.GetString(mongoDBCache.Get("username"));
            Console.ReadKey();
        }
    }
}
