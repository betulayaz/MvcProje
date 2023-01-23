using MvcProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ProjectController : Controller
    {
        MvcProjeDbEntities db = new MvcProjeDbEntities();

        [Route("yazilim-projeleri")]
        public ActionResult SoftwareProject()
        {
            return View(db.tbl_project.Where(s => s.ProjectCategory == "yazilim-projeleri").ToList());
        }
        [Route("ik-projeleri")]
        public ActionResult HrProject()
        {
            return View(db.tbl_project.Where(s => s.ProjectCategory == "ik-projeleri").ToList());
        }
        [Route("finans-projeleri")]
        public ActionResult FinanceProject()
        {
            return View(db.tbl_project.Where(s => s.ProjectCategory == "finans-projeleri").ToList());
        }
        [Route("proje-detay/{id?}")]
        public ActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            tbl_project project = db.tbl_project.Find(id);
            return View(project);
        }
    }
}