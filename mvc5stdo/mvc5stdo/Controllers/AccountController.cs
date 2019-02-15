using mvc5stdo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc5stdo.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            ViewBag.LoginState = "登录前";
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fo) {
            string log = fo["user"];
            ViewBag.LoginState ="欢迎 "+ log+ " 早上好！";
            return View();
        }

        /// <summary>
        /// 通过Json返回数据到前端
        /// </summary>
        /// <returns></returns>
        public JsonResult _StudentList(string utd,string y)
        {

            List<Student> stuModel = new List<Student>()
            {
            new Student() {
                ID=1,
                Name=utd,
                Age=1500,
                Sex="男"
            },
            new Student()
            {
                ID=2,Name=y,Age=2000,Sex="男"
            }
            };


            return Json(stuModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SavePicture(string picString, string id)
        {
            var tmpArr = picString.Split(',');
            byte[] bytes = Convert.FromBase64String(tmpArr[1]);
            MemoryStream ms = new MemoryStream(bytes);
            ms.Write(bytes, 0, bytes.Length);
            var img = Image.FromStream(ms, true);

            var path = System.AppDomain.CurrentDomain.BaseDirectory;
            var imagesPath = System.IO.Path.Combine(path, @"Uploads\AgrCorporations\" + id + "\\");
            if (!System.IO.Directory.Exists(imagesPath))
                System.IO.Directory.CreateDirectory(imagesPath);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".png";
            var bitImage = GetThumbnailImage(img, 400, 300);
            bitImage.Save(imagesPath + fileName);
            bool isOk = true;
            string msg = imagesPath + fileName;
            return Json(new { suc = isOk, msg = msg });
        }

        //图片压缩
        public static Image GetThumbnailImage(Image srcImage, int width, int height)
        {
            Image bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(srcImage, new Rectangle(0, 0, width, width),
                new Rectangle(0, 0, srcImage.Width, srcImage.Height),
                GraphicsUnit.Pixel);

            return bitmap;
        }



	}
}