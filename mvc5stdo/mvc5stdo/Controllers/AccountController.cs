using mvc5stdo.Models;
using System;
using System.Collections.Generic;
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

	}
}