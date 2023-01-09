using MvcProje.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ProjectAdminController : Controller
    {
        MvcProjeDbEntities db = new MvcProjeDbEntities();
        [Authorize(Roles = "Admin")]
        public ActionResult Listing()
        {
            return View(db.tbl_project.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbl_project project, HttpPostedFileBase File)
        {

            project.ProjectAddedDatetime = DateTime.Now;
            project.ProjectAddedAdmin = "";
            project.ProjectStatus = 1;
            if (File != null)
            {
                FileInfo fileinfo = new FileInfo(File.FileName);
                WebImage img = new WebImage(File.InputStream);
                string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                img.Resize(225, 180, false, false);
                string route = "~/images/" + uzanti;
                img.Save(Server.MapPath(route));
                project.ProjectImg = "/images/" + uzanti;
            }
            
            db.tbl_project.Add(project);
            db.SaveChanges();

            return RedirectToAction("Listing");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {        
            if (id == null)
            {
                return HttpNotFound();
            }
            tbl_project project = db.tbl_project.Find(id);
            db.tbl_project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Listing");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            tbl_project project = db.tbl_project.Find(id);
            return PartialView(project);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            tbl_project project = db.tbl_project.Find(id);
            return View(project);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_project project, HttpPostedFileBase File)
        {
            if (project != null)
            {
                db.Entry(project).State = System.Data.Entity.EntityState.Modified;  
                db.Entry(project).Property(m => m.ProjectAddedAdmin).IsModified = false;
                db.Entry(project).Property(m => m.ProjectAddedDatetime).IsModified = false;

                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string route = "~/images/" + uzanti;

                    //klasore kaydetme.
                    img.Save(Server.MapPath(route));
                    project.ProjectImg = "/images/" + uzanti;
                }
                else
                {
                    db.Entry(project).Property(m => m.ProjectImg).IsModified = false;
                }

            }
            db.SaveChanges();
            return RedirectToAction("Listing");
        }
    }
}