using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Common.Common
{
    public static class DateTimeHelper
    {
        public static DateTime GetStartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime GetEndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
    }
}
