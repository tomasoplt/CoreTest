using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Business
{
    public class TestDateService : IDateService
    {
        public DateTime Today
        {
            get
            {
                return new DateTime(2017, 3, 21);
            }
        }
    }
}
