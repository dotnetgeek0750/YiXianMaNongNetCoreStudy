using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyConsole.HHKeyValue
{
    public class HHKeyValueConfigurationProvider : ConfigurationProvider
    {
        public string FilePath { get; set; }
        public HHKeyValueConfigurationProvider(string filePath)
        {
            FilePath = filePath;
        }

        public override void Load()
        {
            var configArray = File.ReadLines(this.FilePath);
            var dict = new Dictionary<string, string>();
            foreach (var item in configArray)
            {
                var val = item.Split('=');
                if (val.Length == 2)
                {
                    dict.Add(val[0], val[1]);
                }
            }
            this.Data = dict;
        }
    }
}
