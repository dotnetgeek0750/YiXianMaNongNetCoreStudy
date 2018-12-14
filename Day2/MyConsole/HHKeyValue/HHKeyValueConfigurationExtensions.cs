using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole.HHKeyValue
{
    public static class HHKeyValueConfigurationExtensions
    {
        public static IConfigurationBuilder AddHHKeyValueFile(this IConfigurationBuilder builder, string path)
        {
            builder.Add(new HHKeyValueConfigurationSource(path));
            return builder;
        }
    }
}
