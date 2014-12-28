using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zxl.ConsolePro.Pro
{
    public class ProIO
    {
        public static void ReadFromTxt()
        {
            string readPath = @"f:\result.csv";
            char splitChar = ',';
            #region read from txt
            using (StreamReader sr = new StreamReader(readPath))
            {
                string line = "";
                string[] col;
                while (line != null)
                {
                    // CampaignId,AdSpaceId,VisitorId,RequestTime,ClientIP,StateId,CityId,RefererHost
                    line = sr.ReadLine();
                    if (line == null || line.Length == 0) break;
                    try
                    {
                        col = line.Split(splitChar);
                        // TODO: col[0...8]
                    }
                    catch { throw; }
                }
            }
            #endregion
        }

        public static void WriteToTxt()
        {
            Dictionary<int, string> dict = new Dictionary<int,string>(); // dataSource
            string writePath = @"f:\result.csv";
            #region Write to txt
            using (FileStream fs = new FileStream(writePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("广告位ID,广告位,曝光数,点击数,曝光UV,点击UV");
                    foreach (var item in dict)
                    {
                        sw.WriteLine("{0},{1}", item.Key,item.Value);
                    }
                }
            }
            #endregion
        }
    }
}
