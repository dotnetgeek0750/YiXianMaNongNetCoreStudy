﻿using ClassLibrary.Common;
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

            while (true)
            {

            }

        }
    }
}
