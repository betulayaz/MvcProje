using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ProjectController : Controller
    {
        [Route("yazilim-projeleri")]
        public ActionResult SoftwareProject()
        {
            return View();
        }
        [Route("ik-projeleri")]
        public ActionResult HrProject()
        {
            return View();
        }
        [Route("finans-projeleri")]
        public ActionResult FinanceProject()
        {
            return View();
        }
        [Route("Proje-detay")]
        public ActionResult Detail()
        {
            return View();
        }
    }
}