using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppPolly
{
    public class BulkheadTest
    {
        public static void Run()
        {
            //声明最大并发执行线程数、线程队列数
            var policy = Policy.Bulkhead(5, 10);
            Parallel.For(0, 100, (i) =>
            {
                var result = policy.Execute<string>(() =>
                {
                    return GetHtml("http://baidu.com");
                });
                Console.WriteLine($"成功获取到数据：{result}");
            });
            Console.ReadKey();
        }

        /// <summary>
        /// 获取页面内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            var html = string.Empty;
            try
            {
                var webClient = new WebClient();
                html = webClient.DownloadString(url);
                html = html.Substring(0, 17);
                Console.WriteLine($"当前线程ID={Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw;
            }
            return html;
        }
    }
}
