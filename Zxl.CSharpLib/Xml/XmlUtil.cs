using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Xml.Xsl;
using System.Web;


namespace Zxl.CSharpLib.Xml
{
    public class XmlUtil
    {
        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <param name="xmlNode">节点</param>
        /// <param name="xmlNodeAttribute">节点的属性</param>
        /// <returns>节点值</returns>
        public static string GetXmlNodeValue(string xmlFilePath, string xmlNode)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.Load(xmlFilePath);
                XmlNode node = xdoc.SelectSingleNode(xmlNode);
                if (node == null)
                {
                    return null;
                }
                return node.InnerXml;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取xml节点的属性值
        /// </summary>
        /// <param name="xmlFilePath">xml文件路径</param>
        /// <param name="xmlNodePath">节点</param>
        /// <param name="xmlNodeAttribute">节点属性</param>
        /// <returns>节点的属性值</returns>
        public static string GetXmlNodeAttributeValue(string xmlFilePath, string xmlNode, string xmlNodeAttribute)
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.Load(xmlFilePath);
                XmlNode node = xdoc.SelectSingleNode(xmlNode);
                if (node == null)
                {
                    return null;
                }
                foreach (XmlAttribute attr in node.Attributes)
                {
                    if (attr.Name == xmlNodeAttribute)
                    {
                        return attr.Value;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //导出到Xml
        public static void ExportToXml(XDocument xDoc, string fileName)
        {
            var dstMemoryStream = new MemoryStream();
            var xmlWriter = XmlWriter.Create(dstMemoryStream);
            xDoc.Save(xmlWriter);
            xmlWriter.Flush();
            dstMemoryStream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[dstMemoryStream.Length];
            dstMemoryStream.Read(buffer, 0, (int)dstMemoryStream.Length);
            string strContent = Encoding.UTF8.GetString(buffer);
            dstMemoryStream.Flush();
            dstMemoryStream.Close();

            ResponseFile(strContent, fileName);
        }
        public static void ResponseFile(string content, string fileName)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "utf-8";
            string userAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();

            if (userAgent.Contains("firefox"))
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            }
            else
            {
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(fileName));
            }

            HttpContext.Current.Response.AddHeader("Content-Length", byteArray.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.BinaryWrite(byteArray);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportFromXmlToExcel(XDocument xDoc, string templateName, string fileName)
        {
            var dstMemoryStream = new MemoryStream();
            var xmlWriter = XmlWriter.Create(dstMemoryStream);

            if (xmlWriter != null)
            {
                var xmlReader = xDoc.CreateReader();
                var xmlTran = new XslCompiledTransform();
                var filepath = HttpContext.Current.Server.MapPath("~/ExcelTemplate/" + templateName);
                xmlTran.Load(filepath);
                xmlTran.Transform(xmlReader, xmlWriter);
            }

            dstMemoryStream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[dstMemoryStream.Length];
            dstMemoryStream.Read(buffer, 0, (int)dstMemoryStream.Length);

            string strContent = Encoding.UTF8.GetString(buffer);
            dstMemoryStream.Flush();
            dstMemoryStream.Close();

            string content = strContent.Replace("\r\n", "&#10;");
            ResponseFile(content, fileName);

        }
    }
}
