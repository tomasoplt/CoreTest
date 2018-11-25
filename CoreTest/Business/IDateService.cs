using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Business
{
    public interface IDateService
    {
        DateTime Today { get; }
    }
    public class DateService : IDateService
    {
        public DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
        }
    }
}
