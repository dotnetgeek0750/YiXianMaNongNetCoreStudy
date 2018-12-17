using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ClassLibrary.Common
{
    public static class ConfigurationManager
    {
        public static IConfigurationRoot Configuration
        {
            get
            {
                //加载配置
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(string.Format("appsetting.{0}.json", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")), optional: true, reloadOnChange: true)
                        .AddJsonFile(string.Format("ops.{0}.json", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")), optional: true, reloadOnChange: true)
                        .Build();
                return configuration;
            }
        }
    }
}
