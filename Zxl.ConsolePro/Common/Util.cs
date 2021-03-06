﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zxl.ConsolePro.Common
{
    public class Util
    {
        #region IpConvertor

        public static int ConvertIpToInt(string ipStr)
        {
            System.Net.IPAddress ip;
            System.Net.IPAddress.TryParse(ipStr, out ip);
            if (ip == null)
                return 0;
            byte[] bytes = ip.GetAddressBytes();
            return (System.Convert.ToInt32(bytes[3]) << 24) | (System.Convert.ToInt32(bytes[2]) << 16)
                | (System.Convert.ToInt32(bytes[1]) << 8) | System.Convert.ToInt32(bytes[0]);
        }
        public static string ConvertIntToIp(int ip)
        {
            return IntegerToIP(ip).ToString();
        }
        public static System.Net.IPAddress IntegerToIP(int ip)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(ip % 256);
            bytes[1] = (byte)((ip >> 8) % 256);
            bytes[2] = (byte)((ip >> 16) % 256);
            bytes[3] = (byte)((ip >> 24) % 256);

            return new System.Net.IPAddress(bytes);
        }

        #endregion
    }
}
