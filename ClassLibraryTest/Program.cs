using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  PerisanDateHelper;

namespace ClassLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new DateBase();
            var yearMonthDay = x.GetYearMonthDay("951003");
            var xx = 2;
        }
    }
}
