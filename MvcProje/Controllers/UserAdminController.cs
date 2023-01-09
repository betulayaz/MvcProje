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
    public class UserAdminController : Controller
    {
        MvcProjeDbEntities db = new MvcProjeDbEntities();
        [Authorize(Roles = "Admin")]
        public ActionResult Listing()
        {
            //Kullanıcıları listeleme.
            return View(db.tbl_user.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_user user, HttpPostedFileBase File)
        {
            //Kullanıcı var mı email ile kontrol ediyoruz.
            var userExist = db.tbl_user.Any(m => m.Email == user.Email);
            if (userExist == false)
            {
                user.AddedDate = DateTime.Now;
                user.AddedBy = "";
                
                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString()+ fileinfo.Extension).ToLower();
                    img.Resize(225,180,false,false);
                    string route = "~/images/users/" + uzanti;

                    //klasore kaydetme.
                    img.Save(Server.MapPath(route));
                    user.Image = "/images/users/" + uzanti;
                }
                //dbye kaydetme.
                db.tbl_user.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("Listing");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            //Kullanıcı silme.
            if (id == null)
            {
                return HttpNotFound();
            }
            tbl_user user = db.tbl_user.Find(id);
            db.tbl_user.Remove(user);
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
            tbl_user user = db.tbl_user.Find(id);
            return PartialView(user);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            tbl_user user = db.tbl_user.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(tbl_user user, HttpPostedFileBase File)
        {
            if (user != null)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;  //güncelleme işlemine izin vermek için gerekli.
                db.Entry(user).Property(m => m.AddedBy).IsModified = false;
                db.Entry(user).Property(m => m.AddedDate).IsModified = false;

                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string route = "~/images/users/" + uzanti;

                    //klasore kaydetme.
                    img.Save(Server.MapPath(route));
                    user.Image = "/images/users/" + uzanti;
                }
                else
                {
                    db.Entry(user).Property(m => m.Image).IsModified = false;
                }
                user.ModifyBy = "";
                user.ModifyDate = DateTime.Now;
            }
            db.SaveChanges();
            return RedirectToAction("Listing");
        }
    }
}