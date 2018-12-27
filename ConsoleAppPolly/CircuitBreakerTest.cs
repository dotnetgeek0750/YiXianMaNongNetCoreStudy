using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace ConsoleAppPolly
{
    public class CircuitBreakerTest
    {
        public static void Run()
        {
            //如果当前连续有两个异常，那么触发熔断，10s内不能调用，10s之后重新调用
            //一旦调用成功，熔断就解除了
            var policy = Policy.Handle<WebException>()
                .CircuitBreaker(4, TimeSpan.FromSeconds(10), (ex, timespan, context) =>
                {
                    //触发熔断
                    Console.WriteLine($"{DateTime.Now}熔断触发:{timespan}");
                }, (context) =>
                {
                    //恢复
                    Console.WriteLine($"{DateTime.Now}熔断恢复");
                });

            var errCount = 1;
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    var msg = policy.Execute<string>((context, token) =>
                    {
                        var url = "http://locxx332424ddxxdddt.com";
                        if (errCount > 10)
                        {
                            url = "http://baidu.com";
                        }
                        return GetHtml(url);
                    }, new Dictionary<string, object>() { { i.ToString(), i.ToString() } }, CancellationToken.None);

                    Console.WriteLine("logic:" + msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now}业务逻辑异常：{ex.Message}");
                    Thread.Sleep(2000);
                    errCount++;
                }
            }
        }


        /// <summary>
        /// 获取页面内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            var html = string.Empty;
            try
            {
                var webClient = new WebClient();
                html = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                throw;
            }
            return html;
        }
    }
}
