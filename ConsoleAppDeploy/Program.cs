using ClassLibrary.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                //每次执行业务操作之前，判断当前CancellationTokenSource的标识是否执行了Cancel
                //如果没有被Cancel则继续进行下一个业务逻辑，保证了不会执行了一半被退出
                while (!tokenSource.IsCancellationRequested)
                {
                    Console.WriteLine($"{DateTime.Now}：业务逻辑处理中");
                    Thread.Sleep(1000);
                }
            }).ContinueWith(t =>
            {
                Console.WriteLine("服务安全退出");
                Environment.Exit(0);//强制退出
            });
            Console.WriteLine("服务成功开启");

            //如果配置为N，则继续进行Task的业务操作
            //如果配置为Y，则执行CancellationTokenSource的取消操作
            var config = ConfigurationManager.Configuration["isquit"];
            while (!"Y".Equals(config, StringComparison.OrdinalIgnoreCase))
            {
                Thread.Sleep(1000);
            }
            tokenSource.Cancel();
        }
    }
}
