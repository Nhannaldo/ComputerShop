using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
namespace Website.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        dbWebsiteDataContext db = new dbWebsiteDataContext();
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(FormCollection f)
        {
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
             USER ad = db.USERs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Login", "Admin");
        }
    }
}