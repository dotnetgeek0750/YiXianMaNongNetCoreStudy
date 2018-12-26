using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace IOCConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            //Transient_ServicesLifeCycle();
            //Singleton_ServicesLifeCycle();
            Scoped_ServicesLifeCycle();

            Console.ReadKey();
        }


        static void IOCLoggin()
        {
            //通常的高耦合使用方法
            //var pig=new Pig();

            //IOC容器
            ServiceCollection services = new ServiceCollection();
            //注册接口与实现类
            services.AddTransient<IFly, Pig>(); //瞬时的
            services.AddSingleton<IFly, Pig>(); //单例的
            services.AddScoped<IFly, Pig>();    //作用域

            //注入Loggin服务到当前容器
            services.AddLogging();

            var provider = services.BuildServiceProvider();

            //配置Log输出地--Console
            provider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);

            var fly = provider.GetService<IFly>();
            fly.Fly();
        }


        /// <summary>
        /// 瞬时的生命周期，GetService几次就new几次
        /// </summary>
        static void Transient_ServicesLifeCycle()
        {
            Console.WriteLine("Transient_ServicesLifeCycle");
            //IOC容器
            ServiceCollection services = new ServiceCollection();
            //注册接口与实现类
            services.AddTransient<IFly, Pig>(); //瞬时的
            //注入Loggin服务到当前容器
            services.AddLogging();

            var provider = services.BuildServiceProvider();

            var fly = provider.GetService<IFly>();
            fly = provider.GetService<IFly>();
            fly.Fly();
        }

        /// <summary>
        /// 单例的生命周期，只new一次
        /// </summary>
        static void Singleton_ServicesLifeCycle()
        {
            Console.WriteLine("Singleton_ServicesLifeCycle");
            //IOC容器
            ServiceCollection services = new ServiceCollection();
            //注册接口与实现类
            services.AddSingleton<IFly, Pig>(); //瞬时的
            //注入Loggin服务到当前容器
            services.AddLogging();

            var provider = services.BuildServiceProvider();

            var fly = provider.GetService<IFly>();
            fly = provider.GetService<IFly>();
            fly.Fly();
        }


        /// <summary>
        /// 作用域生命周期，一个作用域下new一次
        /// </summary>
        static void Scoped_ServicesLifeCycle()
        {
            Console.WriteLine("Scoped_ServicesLifeCycle");
            //IOC容器
            ServiceCollection services = new ServiceCollection();
            //注册接口与实现类
            services.AddScoped<IFly, Pig>(); //作用域
                                             //注入Loggin服务到当前容器
            services.AddLogging();

            var provider = services.BuildServiceProvider();

            //创造2个作用域，每个作用域下都有provider
            var scope1 = provider.CreateScope();
            var scope2 = provider.CreateScope();

            //每个作用域下都有provider，通过provider.GetService
            var fly1 = scope1.ServiceProvider.GetService<IFly>();
            var fly2 = scope2.ServiceProvider.GetService<IFly>();
        }

    }
}
