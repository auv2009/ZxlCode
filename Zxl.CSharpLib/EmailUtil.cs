using System;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace Zxl.CSharpLib
{
    public class EmailUtil
    {
        // email address separator
        const char SEMICOLON = ';';

        // hard code
        public static void Send(string subject, string body)
        {
            const string MAIL_HOST = "mail.adchina.com";
            const string MAIL_USER_NAME = "monitor";
            const string MAIL_PASSWORD = "monitor@data";
            const string MAIL_FROM = "monitor@adchina.com";
            //const string MAIL_TO = "justin.zhang@adchina.com";
            //const string MAIL_CC = "xianming.wan@adchina.com,wei.jia@adchina.com,wesley.sun@adchina.com,eric.zhang@adchina.com";
            const string MAIL_TO = "eric.zhang@adchina.com";
            const string MAIL_CC = "eric.zhang@adchina.com";

            MailMessage message = new MailMessage(MAIL_FROM, MAIL_TO);
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;
            message.Priority = MailPriority.Normal;
            message.CC.Add(MAIL_CC);

            SmtpClient mailClient = new SmtpClient(MAIL_HOST) { Credentials = new NetworkCredential(MAIL_USER_NAME, MAIL_PASSWORD) };
            mailClient.Send(message);
        }

        // 需要身份验证
        public static void Send(
            string host,
            string userName,
            string password,
            string mailFrom,
            string mailTo,
            string mailCc,
            string mailBcc,
            Encoding encoding,
            MailPriority priority,
            string subject, 
            string body,
            bool isBodyHtml)
        {
            MailMessage message = new MailMessage();
            message.Priority = MailPriority.Normal;
            message.From = new MailAddress(mailFrom);
            // mail to
            string[] toArray = mailTo.Split(SEMICOLON);
            foreach (string addr in toArray)
            { 
                message.To.Add(new MailAddress(addr));
            }
            // mail cc
            if (!string.IsNullOrEmpty(mailCc))
            {
                string[] ccArray = mailCc.Split(SEMICOLON);
                foreach (string addr in ccArray)
                {
                    message.CC.Add(new MailAddress(addr));
                }
            }
            // mail bcc
            if (!string.IsNullOrEmpty(mailBcc))
            {
                string[] bccArray = mailBcc.Split(SEMICOLON);
                foreach (string addr in bccArray)
                {
                    message.Bcc.Add(new MailAddress(addr));
                }
            }
            // subject
            message.Subject = subject;
            message.SubjectEncoding = encoding;
            // body
            message.Body = body;
            message.BodyEncoding = encoding;
            message.IsBodyHtml = isBodyHtml;            

            SmtpClient smtpClient = new SmtpClient(host) { Credentials = new NetworkCredential(userName, password) };
            smtpClient.Send(message);
        }

        // 无需身份验证
        public static void Send(
            string host,
            string mailFrom,
            string mailTo,
            string mailCc,
            string mailBcc,
            Encoding encoding,
            MailPriority priority,
            string subject,
            string body,
            bool isBodyHtml)
        {
            MailMessage message = new MailMessage();
            message.Priority = MailPriority.Normal;
            message.From = new MailAddress(mailFrom);
            // mail to
            string[] toArray = mailTo.Split(SEMICOLON);
            foreach (string addr in toArray)
            {
                message.To.Add(new MailAddress(addr));
            }
            // mail cc
            if (!string.IsNullOrEmpty(mailCc))
            {
                string[] ccArray = mailCc.Split(SEMICOLON);
                foreach (string addr in ccArray)
                {
                    message.CC.Add(new MailAddress(addr));
                }
            }
            // mail bcc
            if (!string.IsNullOrEmpty(mailBcc))
            {
                string[] bccArray = mailBcc.Split(SEMICOLON);
                foreach (string addr in bccArray)
                {
                    message.Bcc.Add(new MailAddress(addr));
                }
            }
            // subject
            message.Subject = subject;
            message.SubjectEncoding = encoding;
            // body
            message.Body = body;
            message.BodyEncoding = encoding;
            message.IsBodyHtml = isBodyHtml;

            //string host = "mail.adchina.com";
            SmtpClient smtpClient = new SmtpClient(host);
            smtpClient.Send(message);
        }

        // 无需身份验证，只有收件人，无cc&bcc，utf-8编码，html格式
        public static void Send(
            string host,
            string mailFrom,
            string mailTo,
            string subject,
            string body)
        {
            MailMessage message = new MailMessage();
            message.Priority = MailPriority.Normal;
            message.From = new MailAddress(mailFrom);
            // mail to
            string[] toArray = mailTo.Split(SEMICOLON);
            foreach (string addr in toArray)
            {
                message.To.Add(new MailAddress(addr));
            }
            // subject
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            // body
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            //string host = "mail.adchina.com";
            SmtpClient smtpClient = new SmtpClient(host);
            smtpClient.Send(message);
        }

        // 需要身份验证，只有收件人，无cc&bcc，utf-8编码，html格式
        public static void Send(
            string host,
            string userName,
            string password,
            string mailFrom,
            string mailTo,
            string subject,
            string body)
        {
            MailMessage message = new MailMessage();
            message.Priority = MailPriority.Normal;
            message.From = new MailAddress(mailFrom);
            // mail to
            string[] toArray = mailTo.Split(SEMICOLON);
            foreach (string addr in toArray)
            {
                message.To.Add(new MailAddress(addr));
            }
            // subject
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
            // body
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            //string host = "mail.adchina.com";
            SmtpClient smtpClient = new SmtpClient(host) { Credentials = new NetworkCredential(userName, password) };
            smtpClient.Send(message);
        }
    }
}
