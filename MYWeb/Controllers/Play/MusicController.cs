using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace DawnBlog.Web.Controllers.Play
{
    public class MusicController : Controller
    {
        // GET: /Music/
        public ActionResult Index()
        {
            return View();
        }

        //异步上传,返回json
        public String Upload()
        {
            var file = Request.Files["mp3"];
            dynamic res = new System.Dynamic.ExpandoObject();

            if(file == null)
            {
                res.UploadSuccess = false;
                res.UploadMessage = "上传失败";
            }
            else
            {
                var path = Server.MapPath("~/Content/Upload/");
                //file.FileName 完全限定名,包括客户端路径
                var filename = System.IO.Path.GetFileName(file.FileName);
                path = path + filename;

                file.SaveAs(path);

                res.UploadSuccess = true;
                res.Message = "上传成功";
                res.Path = "http://magicdawn.apphb.com/Content/Upload/" + filename;//
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(res);
        }

        public string Uptoken()
        {
            //读配置文件
            Qiniu.Conf.Config.Init();
            var policy = new Qiniu.RS.PutPolicy("ranger");
            return policy.Token();
        }
    }
}
