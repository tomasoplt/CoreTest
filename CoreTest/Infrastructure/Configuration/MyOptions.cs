using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Infrastructure.Configuration
{
    public class MyOptions
    {
        public string MySimpleConfiguration { get; set; }
        public MySubOptions MyComplexConfiguration { get; set; }
    }
    public class MySubOptions
    {
        public string Username { get; set; }
        public int Age { get; set; }
        public bool IsMvp { get; set; }
    }
}
