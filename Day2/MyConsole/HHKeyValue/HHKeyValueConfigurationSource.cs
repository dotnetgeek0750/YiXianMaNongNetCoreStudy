using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole.HHKeyValue
{
    public class HHKeyValueConfigurationSource : IConfigurationSource
    {
        public string FilePath { get; set; }
        public HHKeyValueConfigurationSource(string filePath)
        {
            this.FilePath = filePath;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new HHKeyValueConfigurationProvider(this.FilePath);
        }
    }
}
