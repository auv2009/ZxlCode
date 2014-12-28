using System;

namespace Zxl.CSharpLib
{
    public class StringUtil
    {
        //double dd = 0.5351526;
        //string str1 = dd.ToString("f5");
        //fN 保留N位，四舍五入
        public static string ToDecimal(double decimalPlaces)
        {
            string fomat = "f" + decimalPlaces;
            return decimalPlaces.ToString(fomat);
        }
    }
}
