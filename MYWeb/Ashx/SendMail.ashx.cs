using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DawnBlog.Web.Ashx
{
    /// <summary>
    /// SendMail.ashx
    /// </summary>
    public class SendMail : IHttpHandler
    {
        //访问地址给xx邮箱发邮件
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //接受参数
            var to = context.Request["to"];
            var body = context.Request["body"];
            var subject = context.Request["subject"];

            //1.使用方法
            if(to == null && body == null)
            {
                context.Response.Write("发送格式为\nto=给哪个邮箱发&subject=主题&body=正文");
                return;
            }

            //2.参数
            if(string.IsNullOrEmpty(to))
            {
                context.Response.Write("to参数不能为空");
                return;
            }
            if(string.IsNullOrEmpty(body))
            {
                context.Response.Write("body参数不能为空");
                return;
            }
            //判断to邮箱格式
            if(!IsEmailAddress(to))
            {
                context.Response.Write("to格式不正确...");
                return;
            }
            //subject 默认为datetime(yyyy-MM-dd HH:mm:ss)
            if(string.IsNullOrEmpty(subject)) subject = DateTime.Now.ToStringX();

            //创建客户端
            SmtpClient client = new SmtpClient("smtp.163.com",25);
            client.Credentials = new NetworkCredential("public_mail_csharp@163.com","mimahenhaoji");

            //创建消息
            MailMessage msg = new MailMessage("public_mail_csharp@163.com",HttpUtility.UrlDecode(to));
            msg.Subject = HttpUtility.UrlDecode(subject);
            msg.Body = HttpUtility.UrlDecode(body);

            //发送
            client.Send(msg);

            context.Response.Write("发送成功\n");
            context.Response.Write("------\n");
            context.Response.Write("to : {0}\n".format(HttpUtility.UrlDecode(to)));
            context.Response.Write("subject : {0}\n".format(HttpUtility.UrlDecode(subject)));
            context.Response.Write("body : {0}\n".format(HttpUtility.UrlDecode(body)));
        }

        bool IsEmailAddress(string addr)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(addr,@"\w+@\w+\.\w");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}