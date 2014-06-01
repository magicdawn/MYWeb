using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace DawnBlog.Web.Code
{
    //迅雷VIP相关
    public class ThunderHelper
    {
        //迅雷VIP账号密码 XML文件
        public static readonly string XmlDataPath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\ThunderData.xml";
        //匹配的正则
        public static readonly string RegexPattern =
            File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\RegexPattern.txt",
            System.Text.Encoding.GetEncoding("GB2312")
        );

        //获取VIP账号,返回Dictionary
        public static Dictionary<string,string> GetThunderVip()
        {
            Regex regex = new Regex(RegexPattern);
            string url = "http://xunlei6.com/2.htm";
            string html;
            try
            {
                using(var stream = new WebClient().OpenRead(url))
                {
                    using(var sr = new StreamReader(stream,System.Text.Encoding.GetEncoding("GB2312")))
                    {
                        html = sr.ReadToEnd();
                        html = html.Substring(0,html.IndexOf("----------"));
                    }
                }
            }
            catch(WebException e)
            {
                //上面异常,没联网,读取本地的
                return ReadXml();
            }

            var matches = regex.Matches(html);
            var dict = new Dictionary<string,string>();
            string key,value;
            foreach(Match m in matches)
            {
                key = m.Groups["id"].Value;
                value = m.Groups["pwd"].Value;

                //判断是否存在,若存在,用新密码更新旧密码
                if(dict.ContainsKey(key))
                {
                    dict[key] = value;
                }
                else
                {
                    dict.Add(key,value);
                }
            }
            return dict;
        }

        //写入到XML
        public static void WritrToXml(Dictionary<string,string> dict)
        {
            //<thunder>
            //<data>
            //  <id>1</id>
            //  <pwd>1</pwd>
            //</data>

            XDocument doc = new XDocument();
            doc.Add(
                    new XElement("Thunder",
                    from pair in dict
                    select
                    new XElement("data",
                        new XElement("id",pair.Key),
                        new XElement("pwd",pair.Value)
                    )
                )
            );

            //foreach (var pair in dict)
            //{
            //    //创建节点
            //    var data = new XElement("data");
            //    //添加子节点
            //    data.Add(new XElement("id", pair.Key));
            //    data.Add(new XElement("pwd", pair.Value));
            //    //one 添加到root
            //    doc.Root.Add(data);
            //}
            doc.Save(XmlDataPath);
        }

        //读取xml,返回Dictionary类型
        public static Dictionary<string,string> ReadXml()
        {
            var dict = new Dictionary<string,string>();
            var doc = XDocument.Load(XmlDataPath);
            var datas = doc.Root.Elements("data");
            string id,pwd;
            foreach(var data in datas)
            {
                id = data.Element("id").Value;
                pwd = data.Element("pwd").Value;
                dict.Add(id,pwd);
            }
            return dict;
        }
    }
}