using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DawnBlog.Web.Controllers.Play
{
    public class SimsimiController : Controller
    {
        // 小黄鸡
        // GET: /Simsimi/
        public ActionResult Index()
        {
            return View();
        }

        // 数据中转
        // GET: /Simsimi/Agent?chat=哈哈
        public string Agent()
        {
            var chat = Request["chat"];
            if (chat == null)
            {
                return "未提供关键字";
            }
            else
            {
                return Code.SimsimiHelper.GetAnswer(chat);
            }
        }

    }
}
