using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOCConsole
{
    class Pig : IFly
    {
        ILogger<Pig> logger = null;

        public Pig(ILoggerFactory loggerFactory)
        {
            Console.WriteLine("从构造函数看LifeCycle，输出几次表示New几次");
            logger = loggerFactory.CreateLogger<Pig>();
        }

        public void Fly()
        {
            logger.LogInformation("猪会飞的Debug日志");

            Console.WriteLine("猪会飞");
        }
    }
}
