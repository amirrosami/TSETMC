using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Methods
{
    public static class commonmethod
    {
        public static string ConvertDate(string mydate,string to)
        {
            var result=mydate;
            if (to=="miladi")
            {
                string year = mydate[0].ToString() + mydate[1].ToString() + mydate[2].ToString() + mydate[3].ToString();
                string month = mydate[4].ToString() + mydate[5].ToString();
                string day = mydate[6].ToString() + mydate[7].ToString();
                var calendar = new PersianCalendar();
                var miladitime = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), calendar);
                 result = miladitime.ToString("yyyyMMdd");
                
            }

           
            return result;
           
        }
    }
}
