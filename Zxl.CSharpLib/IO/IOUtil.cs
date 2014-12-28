using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zxl.CSharpLib.IO
{
    public class IOUtil
    {
        public static void WriteLine(string logPath, string msg)
        {
            using (FileStream fs = new FileStream(logPath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(msg);
                }
            }
        }

        /// <summary>
        /// 判断一个路径是文件还是文件夹
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>true:文件夹，false:文件</returns>
        public static bool IsFolder(string fileNamePath)
        {
            FileInfo fileInfo = new FileInfo(fileNamePath);
            if ((fileInfo.Attributes & FileAttributes.Directory) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
