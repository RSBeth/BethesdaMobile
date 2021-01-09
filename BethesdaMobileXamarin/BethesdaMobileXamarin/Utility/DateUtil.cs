using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace BethesdaMobileXamarin.Utility
{
    class DateUtil
    {

        public static string ConvertFormatDateTime(string dt,string formatOld,string formatNew)
        {
            string newDate = DateTime.ParseExact(dt, formatOld, CultureInfo.InvariantCulture).ToString(formatNew);
            return newDate;
        }

        public static DateTime ConvertStringToDate(string dt, string formatOld)
        {
            DateTime newDate = DateTime.ParseExact(dt, formatOld,null);
            return newDate;
        }
    }
}
