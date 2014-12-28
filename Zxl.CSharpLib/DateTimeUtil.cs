using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zxl.CSharpLib
{
    public class DateTimeUtil
    {
        public static DateTime _unixTime = DateTime.Parse("1970-01-01");
        
        /// <summary>
        /// 某一时刻t有两种表示方法：
        /// ①1970-01-01至t时刻间隔的毫秒数(long型, 13位数字, eg:1348502400000)
        /// ②DateTime, 例如yyyy-MM-dd HH:mm:ss
        /// 本方法把时间格式①转换为时间格式②
        /// </summary>
        /// <param name="milliseconds">1970-01-01 ~ t时刻间隔的毫秒数</param>
        /// <returns></returns>
        public static DateTime From1970MillisecondsToDatetime(long milliseconds)
        {
            DateTime dt = _unixTime.AddMilliseconds(milliseconds); //ts.TotalMilliseconds
            return dt;
        }

        public static long FromDatetimeTo1970Milliseconds(DateTime dt)
        {
            //DateTime RequestTime = DateTime.Parse("2012-09-24 16:00:00");
            TimeSpan ts = dt.Subtract(_unixTime);
            return (long)ts.TotalMilliseconds;            
        }
    }
}
