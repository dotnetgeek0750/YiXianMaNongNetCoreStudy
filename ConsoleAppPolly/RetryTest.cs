using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleAppPolly
{
    public class RetryTest
    {
        /// <summary>
        /// 重试三次
        /// 第一次失败等待1秒，第二次失败等待3秒，第三次重试成功
        /// </summary>
        public static void Run()
        {
            var retryPolicy = Policy.Handle<WebException>()
                .WaitAndRetry(new List<TimeSpan>
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5),
                }, (ex, dt) =>
                {
                    Console.WriteLine($"{ex.Message} {dt}");
                });
            int count = 0;
            var html = retryPolicy.Execute(() =>
            {
                var url = "http://localhost.com";
                if (count == 2)
                {
                    url = "http://baidu.com";
                }
                return GetHtml(url, ref count);
            });
            Console.WriteLine(html);
        }

        /// <summary>
        /// 获取页面内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetHtml(string url, ref int count)
        {
            var html = string.Empty;
            try
            {
                var webClient = new WebClient();
                html = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                count++;
                throw;
            }
            return html;
        }
    }
}
