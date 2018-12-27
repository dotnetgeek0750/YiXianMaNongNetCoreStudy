using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleAppPolly
{
    public class TimeoutTest
    {
        public static void Run()
        {
            var cancellationToken = new CancellationToken();
            //10s后Polly会超时然后让CancellationToken过期
            var policy = Policy.Timeout(10);
            policy.Execute((token) =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine($"是否已经被取消了：{token.IsCancellationRequested}");
                    Thread.Sleep(1000);
                }
            }, cancellationToken);
        }
    }
}
