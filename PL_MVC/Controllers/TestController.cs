using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Test()
        {
            ViewBag.Test = "TEST GET";
            return View();
        }

        [HttpPost]
        public ActionResult Test(string algo)
        {
            ViewBag.Test = "TEST POST";
            return View();
        }
    }
}