using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// 静的コントローラ（ブラウジング）
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// トップページ
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
