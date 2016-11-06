using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public ActionResult Directives()
        {
            return View();
        }

        public ActionResult Databinding()
        {
            return View();
        }

        public ActionResult Templates()
        {
            return View();
        }

        public ActionResult Expressions()
        {
            return View();
        }
        public ActionResult Repeaters()
        {
            return View();
        }

        public ActionResult Repeaters2()
        {
            return View();
        }

        public ActionResult Scope()
        {
            return View();
        }

        public ActionResult Controllers()
        {
            return View();
        }

        public ActionResult Components()
        {
            return View();
        }

        public ActionResult PersonComponent()
        {
            return View();
        }
    }
}