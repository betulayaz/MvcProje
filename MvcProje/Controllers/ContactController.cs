using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ContactController : Controller
    {
        [Route("İletişim")]
        public ActionResult Index()
        {
            ViewBag.Title = "İletişim";
            return View();
        }
    }
}