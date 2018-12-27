using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleAppPolly
{
    public class FallbackTest
    {
        public static void Run()
        {
            var result = Policy<string>.Handle<WebException>()
                .Fallback(() =>
                {
                    //调用失败的话，会执行到这里
                    return "接口失败，这个fake的值给你";
                }).Execute(() =>
                {
                    return GetHtml("http://locahostcooooos33.com");
                });

            Console.WriteLine(result);
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
            }
            catch (Exception ex)
            {
                throw;
            }
            return html;
        }
    }
}
