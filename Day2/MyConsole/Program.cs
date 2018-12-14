using Microsoft.Extensions.Configuration;
using MyConsole.HHKeyValue;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Bind
            //var obj = new Rootobject();
            //configuration.Bind(obj);

            ////Get<T>
            //var newObj = configuration.Get<Rootobject>();


            Console.ReadLine();
        }


        static void ReadJsonAppsetting()
        {
            IConfiguration configuration =
                 new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                .Build();
        }

        static void ReadHHKeyValueFile()
        {
            IConfiguration configuration =
                  new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                 .AddHHKeyValueFile("HHKeyValue.config")
                 .Build();
            Console.WriteLine(configuration["name"]);
        }

    }





}
