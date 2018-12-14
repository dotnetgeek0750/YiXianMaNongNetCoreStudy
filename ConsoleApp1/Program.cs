using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

using AspectCore.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Cache();
        }


        static void Log()
        {
            //IOC 容器
            ServiceCollection services = new ServiceCollection();
            //注册服务
            services.AddDynamicProxy();
            //注册接口及实现类
            services.AddTransient<IMysql, MySql>();
            //要使用AspectCore的BuildAspectCoreServiceProvider，而非.net core内置的BuildServiceProvider
            var provider = services.BuildAspectCoreServiceProvider();
            //获取IMysql接口的实例
            var mysql = provider.GetService<IMysql>();
            //调用被切面的函数
            mysql.GetData(20);

            Console.Read();
        }

        static void Cache()
        {
            //IOC 容器
            ServiceCollection services = new ServiceCollection();
            //注册服务
            services.AddDynamicProxy();
            //注册接口及实现类
            services.AddTransient<IMysql, MySql>();
            //要使用AspectCore的BuildAspectCoreServiceProvider，而非.net core内置的BuildServiceProvider
            var provider = services.BuildAspectCoreServiceProvider();
            //获取IMysql接口的实例
            var mysql = provider.GetService<IMysql>();
            //调用被切面的函数
            var resultFromCode = mysql.GetData(10);
            Console.WriteLine(resultFromCode);

            var resultFromCache = mysql.GetData(10);
            Console.WriteLine(resultFromCache);
            Console.Read();
        }

    }



    /// <summary>
    /// 日志切面
    /// </summary>
    public class MyLogInterceptorAttribute : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            //before
            Console.WriteLine("开始AOP切面记录日志");

            //执行被添加本Attribute特性的函数
            var task = next(context);

            //after
            Console.WriteLine("结束AOP切面记录日志");

            return task;
        }
    }


    /// <summary>
    /// 缓存切面
    /// </summary>
    public class MyCacheInterceptorAttribute : AbstractInterceptorAttribute
    {
        //临时缓存
        private Dictionary<string, string> cacheDict = new Dictionary<string, string>();

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            //用参数ID作为cacheKey
            var cacheKey = string.Join(",", context.Parameters);
            if (cacheDict.ContainsKey(cacheKey))
            {
                //返回缓存中的值
                context.ReturnValue = cacheDict[cacheKey];
                return Task.CompletedTask;
            }
            //执行方法
            var task = next(context);
            //获取执行方法的结果：ReturnValue是一个传递值媒介
            var cacheValue = context.ReturnValue;
            //放入缓存
            cacheDict.Add(cacheKey, "From Cache:" + cacheValue.ToString());
            return task;
        }
    }


    public interface IMysql
    {
        string GetData(int id);
    }

    public class MySql : IMysql
    {
        //[MyLogInterceptor]
        [MyCacheInterceptor]
        public string GetData(int id)
        {
            var msg = $"已经获取到数据id={id}的数据，GetData方法被切面";
            return msg;
        }
    }
}
