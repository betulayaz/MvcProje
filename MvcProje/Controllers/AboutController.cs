using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class AboutController : Controller
    {
       [Route("Hakkımızda")]
        public ActionResult Index()
        {
            ViewBag.Title = "Hakkımızda";
            return View();
        }
    }
}