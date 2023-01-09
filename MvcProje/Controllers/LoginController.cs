using MvcProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProje.Controllers
{
    public class LoginController : Controller
    {
        MvcProjeDbEntities db = new MvcProjeDbEntities();
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_user user)
        {
            var UserIndb = db.tbl_user.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (UserIndb != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Session["Email"] = user.Email.ToString();
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Message = "Hatalı Giriş Bilgileri Girdiniz.Lütfen Kontrol Ediniz.";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}