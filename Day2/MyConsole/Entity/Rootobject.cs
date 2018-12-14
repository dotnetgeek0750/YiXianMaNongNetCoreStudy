using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole.Entity
{
    public class Rootobject
    {
        public Mysql mysql { get; set; }
        public Shopidlist[] shopidlist { get; set; }
    }
}
