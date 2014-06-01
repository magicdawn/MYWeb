using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DawnBlog.Web.Controllers
{
    public class ThunderController : Controller
    {
        // 获取迅雷VIP
        // GET: /Thunder/
        public ActionResult Index()
        {
            ViewBag.Dict = GetDict();
            return View();
        }

        // 强制更新
        // GET: /Thunder/Update
        public RedirectResult Update()
        {
            //重新获取
            ForceUpdate();
            //转到Index
            //if (Request.UrlReferrer != null)
            //{
            //    Response.Redirect(Request.UrlReferrer.ToString(), true);
            //}
            //else
            //{
            //    Response.Redirect("~/Thunder");
            //}
            return Redirect("~/Thunder");
        }

        // 异步版本
        // GET: /Thunder/Async
        public ActionResult Async()
        {
            return View();
        }

        // 帮助,那个兼容模式&IE
        // GET:/Thunder/Help
        public ActionResult Help()
        {
            return View();
        }

        // 获取JSON
        // GET:/Thunder/Json
        public JsonResult Json()
        {
            var dict = GetDict();
            var obj = dict.Select(pair => new {
                id = pair.Key,
                pwd = pair.Value
            });
            return Json(obj,JsonRequestBehavior.AllowGet);
        }

        //获取dict, 强制更新or直接读取
        private Dictionary<string,string> GetDict()
        {
            //1.文件不存在
            if(!System.IO.File.Exists(Code.ThunderHelper.XmlDataPath)) return ForceUpdate();

            //2.比较文件时间    
            var now = DateTime.Now;
            var file = new FileInfo(Code.ThunderHelper.XmlDataPath).LastWriteTime;
            if((now - file).TotalHours > 1) return ForceUpdate();

            //3.内容为空
            var dict = Code.ThunderHelper.ReadXml();
            if(dict.Count == 0) return ForceUpdate();

            //直接读取
            return dict;
        }

        //强制更新
        public Dictionary<string,string> ForceUpdate()
        {
            var dict = Code.ThunderHelper.GetThunderVip();//获取
            Code.ThunderHelper.WritrToXml(dict);//写入XML
            return dict;//返回字典
        }
    }
}