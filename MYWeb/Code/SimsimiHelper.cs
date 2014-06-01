using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace DawnBlog.Web.Code
{
    public static class SimsimiHelper
    {
        public static string GetAnswer(string chat)
        {
            var data = string.Empty;//返回数据
            var url = "http://xhj.douqq.com/bot/chat.php";
            try
            {
                //创建Request
                var req = WebRequest.Create(url) as HttpWebRequest;

                //POST数据
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                using (var reqStream = req.GetRequestStream())
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes("chat=" + chat);
                    reqStream.Write(bytes, 0, bytes.Length);
                }

                //得到请求,再发送
                var res = req.GetResponse() as HttpWebResponse;
                using (var resStream = res.GetResponseStream())
                {
                    using (var sr = new System.IO.StreamReader(resStream))
                    {
                        data = sr.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                data = "抱歉,服务器出错了~小黄鸡不能再陪你玩了~";
            }
            return data;
        }
    }
}